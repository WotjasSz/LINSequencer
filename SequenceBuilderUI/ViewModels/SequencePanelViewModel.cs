using Caliburn.Micro;
using LINSequencerLib;
using LINSequencerLib.BabyLinWrapper;
using LINSequencerLib.Sequence;
using SequenceBuilderUI.Helpers;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SequenceBuilderUI.ViewModels
{
    public  class SequencePanelViewModel : Screen
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        private BindableCollection<SequenceModel> _sequences;
        private BindableCollection<DeviceModel> _devices;
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

        #endregion


        public SequencePanelViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
        }

        public bool CanLoadSequence()
        {
            // Make sure something is selected in both comboboxes
            return true;
        }

        public void LoadSequence(string sequence)
        {
            Debug.WriteLine("Przycisk zadziałał");
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            Sequences = BindableCollectionHelper.ListToBindableCollection(LinSequencer.SequenceList);
            Devices = BindableCollectionHelper.ListToBindableCollection(LinSequencer.DeviceList);
            return base.OnActivateAsync(cancellationToken);
        }
    }
}