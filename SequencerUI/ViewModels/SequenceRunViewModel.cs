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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SequencerUI.ViewModels
{
    public partial class SequenceRunViewModel : ObservableObject
    {
        [ObservableProperty]
        private ISequenceModel _sequence;

        [ObservableProperty]
        private ObservableCollection<DeviceModel>? _devices;

        [ObservableProperty]
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

        [RelayCommand]
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
    }
}
