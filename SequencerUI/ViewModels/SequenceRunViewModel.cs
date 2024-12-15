using CommunityToolkit.Mvvm.ComponentModel;
using LINSequencerLib;
using LINSequencerLib.BabyLinWrapper;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class SequenceRunViewModel : ObservableObject
    {
        [ObservableProperty]
        private ISequenceModel _sequence;

        [ObservableProperty]
        private ObservableCollection<DeviceModel> _devices;

        [ObservableProperty]
        private DeviceModel _currentDevice;
        
        public SequenceRunViewModel()
        {
            Sequence = new SequenceModel();            
        }

        public SequenceRunViewModel(ISequenceModel sequence)
        {
            Sequence = sequence;
            Devices = new ObservableCollection<DeviceModel>(LinSequencer.DeviceList);
        }

        public void UpdateSequence(ISequenceModel sequence)
        {
            Sequence = sequence;            
        }
    }
}
