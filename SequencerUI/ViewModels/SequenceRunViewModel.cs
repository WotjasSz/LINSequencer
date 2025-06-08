using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib;
using LINSequencerLib.BabyLinWrapper;
using LINSequencerLib.Log;
using LINSequencerLib.Sequence;
using LINSequencerLib.SequenceStep;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace SequencerUI.ViewModels
{
    public partial class SequenceRunViewModel : ObservableObject
    {
        [ObservableProperty]
        private ISequenceModel _sequence;

        [ObservableProperty]
        private ObservableCollection<ISeqStep<object>> _sequenceSteps;

        [ObservableProperty]
        private ObservableCollection<StepListItemViewModel> _stepListItems = new ObservableCollection<StepListItemViewModel>();

        [ObservableProperty]
        private int _currentStep;

        [ObservableProperty]
        private int _stepCount;

        [ObservableProperty]
        private ESequenceState _sequenceState;
        
        [ObservableProperty]
        private ESequenceResult _sequenceResult;

        [ObservableProperty]
        private ObservableCollection<DeviceModel>? _devices;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConnectDeviceCommand))]
        private DeviceModel? _currentDevice;
        
        [ObservableProperty]
        private ObservableCollection<LogMessage> _messages;

        [ObservableProperty]
        private bool _isPopupOpen;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RunSequenceCommand))]
        [NotifyCanExecuteChangedFor(nameof(ReloadDeviceListCommand))]
        private bool _isConnected;
        
        [ObservableProperty]
        private bool _isDisconnected;
        
        [ObservableProperty]
        private bool _isRunning;

        [ObservableProperty]
        private bool _isStopped;

        private readonly IMessenger _messenger;
        private readonly LoggerModel _logger;

        public SequenceRunViewModel(IMessenger messenger)
        {
            _messenger = messenger;

            Sequence = new SequenceModel();
            StatusFieldUpdate();
            Sequence.SequenceStatusChanged += Sequence_SequenceStatusChanged;
            ReloadDeviceList();

            _logger = Sequence.Logger;
            _logger.SequenceLog += OnLogGenerated;

            Messages = new ObservableCollection<LogMessage>();

            IsPopupOpen = false;
            ConnectionUpdate();
            StatusUpdate();
            //Messages.CollectionChanged += Messages_CollectionChanged;
        }

        private void Sequence_SequenceStatusChanged(object? sender, ESequenceState e)
        {
            throw new NotImplementedException();
        }

        public SequenceRunViewModel(IMessenger messenger, SequenceModel sequence)
        {
            _messenger = messenger;

            Sequence = sequence;
            StatusFieldUpdate();
            Sequence.SequenceStatusChanged += Sequence_SequenceStatusChanged;
            ReloadDeviceList();

            _logger = Sequence.Logger;
            _logger.SequenceLog += OnLogGenerated;

            Messages = new ObservableCollection<LogMessage>();
            IsPopupOpen = false;
            ConnectionUpdate();
            StatusUpdate();
        }        

        public void UpdateSequence(ISequenceModel sequence)
        {
            Sequence = sequence;            
        }

        #region Commands
        [RelayCommand]
        private void ShowDescription()
        {
            IsPopupOpen = !IsPopupOpen;
        }

        [RelayCommand(CanExecute = nameof(CanExecuteReloadDevice))]
        private void ReloadDeviceList()
        {
            //TODO solve issue with device scanning. Each time when device is connecting data inside are changed.
            // Add kind of device managing to select device which where selected before connection afte connection.
            LinSequencer.CheckAvailableDevice();
            Devices = new ObservableCollection<DeviceModel>(LinSequencer.DeviceList);
        }

        [RelayCommand(CanExecute = nameof(CanExecuteConnectDevice))]
        private void ConnectDevice()
        {
            Sequence.ConnectDevice(CurrentDevice);
            ConnectionUpdate();
        }

        [RelayCommand]
        private void DisconnectDevice()
        {
            Sequence.DisconnectDevice();
            ConnectionUpdate();
        }

        [RelayCommand(CanExecute = nameof(CanExeuteRunSequence))]
        private void RunSequence()
        {
            Sequence.RunAsync(LinSequencer.FunctionList);
            SequenceSteps = new ObservableCollection<ISeqStep<object>>(Sequence.SequenceSteps);
            StepListItems.Clear();
            StepListItems = new ObservableCollection<StepListItemViewModel>(SequenceSteps.Select(step => new StepListItemViewModel(_messenger, step)));
            StatusUpdate();
        }   
        
        [RelayCommand]
        private void CancelSequence()
        {
            Sequence.CancelSequence();
            StatusUpdate();
        }
        #endregion

        #region Events handling
        private void OnLogGenerated(object? sender, LogMessage? msg)
        {
            if (msg != null)
            {
                Application.Current.Dispatcher.BeginInvoke(() =>
                    {
                        Messages.Add(msg);
                    });
            }
        }

        private void Sequence_SequenceStatusChanged(object? sender, EventArgs e)
        {
            StatusFieldUpdate();
            StatusUpdate();
        }

        //TODO Sprawdzić i rozwinąć przewijanie listy do ostatniego elementu
        private void Messages_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var lastItem = Messages.LastOrDefault();
                if (lastItem != null)
                {
                    ScrollToLastItem(lastItem);
                }
            }
        }

        private void ScrollToLastItem(LogMessage lastItem)
        {
            ScrollToItem?.Invoke(this, new ScrollEventArgs { Item = lastItem });
        } 
        #endregion

        private void ConnectionUpdate()
        {
            IsConnected = Sequence.IsConnected;
            IsDisconnected = !IsConnected;
        }

        private void StatusFieldUpdate()
        {
            CurrentStep = Sequence.CurrentStep;
            StepCount = Sequence.StepCount;
            SequenceState = Sequence.State;
            SequenceResult = Sequence.Result;
        }

        private void StatusUpdate()
        {
            IsRunning = Sequence.IsRunning;
            IsStopped = !IsRunning;
        }

        private bool CanExecuteReloadDevice()
        {
            return !IsConnected;
        }

        private bool CanExeuteRunSequence()
        {
            return IsConnected;
        }

        private bool CanExecuteConnectDevice()
        {
            return CurrentDevice != null ? true : false;
        }

        public event EventHandler<ScrollEventArgs> ScrollToItem;

        public class ScrollEventArgs : EventArgs
        {
            public object Item { get; set; }
        }
    }
}
