using Caliburn.Micro;
using LINSequencerLib;
using LINSequencerLib.BabyLinWrapper;
using LINSequencerLib.Sequence;
using SequenceBuilderUI.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SequenceBuilderUI.ViewModels
{
    public class SidePanelViewModel : Screen
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        private BindableCollection<SequenceModel> _sequences;
        private BindableCollection<DeviceModel> _devices;
        private SequenceModel _selectedSequence;
        private DeviceModel _selectedDevice;
        #endregion

        #region Properties
        public BindableCollection<SequenceModel> Sequences
        {
            get { return _sequences; }
            set
            {
                Set(ref _sequences, value);
            }
        }
        public BindableCollection<DeviceModel> Devices
        {
            get { return _devices; }
            set
            {
                Set(ref _devices, value);
            }
        }
        public SequenceModel SelectedSequence
        {
            get { return _selectedSequence; }
            set
            {
                Set(ref _selectedSequence, value);
                NotifyOfPropertyChange(() => CanLoadSequence);
            }
        }
        public DeviceModel SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                Set(ref _selectedDevice, value);
                NotifyOfPropertyChange(() => CanLoadSequence);
            }
        }
        public bool CanLoadSequence
        {
            get
            {
                return SelectedSequence != null & SelectedDevice != null;
            }
        }
        #endregion


        public SidePanelViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
        }

        public void AddSequence()
        {
            int counter = Sequences.Count + 1;
            SequenceModel seq = new SequenceModel(counter, DateTime.Now, "New sequence", "");
            _eventAggregator.PublishOnUIThreadAsync(new SequenceMessage(this, seq)); ;
        }

        public void LoadSequence(string sequence)
        {
            _eventAggregator.PublishOnUIThreadAsync(new SequenceMessage(this, SelectedSequence));
        }

        public void RefreshDeviceList()
        {
            LinSequencer.CheckAvailableDevice();
            Devices = null;
            Devices = new BindableCollection<DeviceModel>(LinSequencer.DeviceList);
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            Sequences = new BindableCollection<SequenceModel>(LinSequencer.SequenceList);
            Devices = new BindableCollection<DeviceModel>(LinSequencer.DeviceList);
            return base.OnActivateAsync(cancellationToken);
        }
    }
}