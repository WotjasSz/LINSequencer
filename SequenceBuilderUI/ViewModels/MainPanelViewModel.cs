using Caliburn.Micro;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SequenceBuilderUI.ViewModels
{
    public class MainPanelViewModel : Conductor<Screen>.Collection.OneActive, IHandle<SequenceModel>
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        private BindableCollection<SequenceModel> _activatedSequences;
        private SequenceViewModel _sequenceView;
        #endregion

        #region Properties
        public BindableCollection<SequenceModel> ActivatedSequences
        {
            get { return _activatedSequences; }
            set 
            { 
                Set(ref _activatedSequences, value); 
            }
        }        

        public SequenceViewModel SequenceView
        {
            get { return _sequenceView; }
            set 
            { 
                Set(ref _sequenceView, value); 
            }
        }

        #endregion

        public MainPanelViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, SequenceViewModel sequenceView)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _sequenceView = sequenceView;

            _eventAggregator.SubscribeOnPublishedThread(this);

            ActivatedSequences = new BindableCollection<SequenceModel>();
            SequenceView.ConductWith(this);
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return base.OnActivateAsync(cancellationToken);
        }

        public Task HandleAsync(SequenceModel message, CancellationToken cancellationToken)
        {             
            return Task.Run(() => ActivatedSequences.Add(message));
        }
    }
}
