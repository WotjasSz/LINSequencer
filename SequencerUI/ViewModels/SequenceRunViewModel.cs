using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib;
using LINSequencerLib.BabyLinWrapper;
using LINSequencerLib.Log;
using LINSequencerLib.Sequence;
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
        private ObservableCollection<DeviceModel>? _devices;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RunSequenceCommand))]
        private DeviceModel? _currentDevice;

        [ObservableProperty]
        private ObservableCollection<LogMessage> _messages;

        private readonly IMessenger _messenger;
        private readonly LoggerModel _logger;

        public SequenceRunViewModel(IMessenger messenger)
        {
            _messenger = messenger;

            Sequence = new SequenceModel();
            Devices = new ObservableCollection<DeviceModel>(LinSequencer.DeviceList);

            Messages = new ObservableCollection<LogMessage>();            
            _logger = Sequence.Logger;
            _logger.SequenceLog += OnLogGenerated;

            Messages.CollectionChanged += Messages_CollectionChanged;
        }       

        public SequenceRunViewModel(IMessenger messenger, SequenceModel sequence)
        {
            _messenger = messenger;

            Sequence = sequence;
            Devices = new ObservableCollection<DeviceModel>(LinSequencer.DeviceList);

            Messages = new ObservableCollection<LogMessage>();
            _logger = Sequence.Logger;
            _logger.SequenceLog += OnLogGenerated;                   
        }

        public void UpdateSequence(ISequenceModel sequence)
        {
            Sequence = sequence;            
        }

        #region Commands
        [RelayCommand]
        private void ReloadDeviceList()
        {
            LinSequencer.CheckAvailableDevice();
            CurrentDevice = null;
            Devices.Clear();
            Devices = new ObservableCollection<DeviceModel>(LinSequencer.DeviceList);            
        }

        [RelayCommand(CanExecute = nameof(CanExeuteRunSequence))]
        private void RunSequence()
        {
            Sequence.RunAsync(CurrentDevice, LinSequencer.FunctionList);
        }        
        #endregion

        private void OnLogGenerated(object? sender, LogMessage? msg) 
        {
            if (msg != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                    {
                        Messages.Add(msg);
                    }); 
            }
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


        private bool CanExeuteRunSequence()
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
