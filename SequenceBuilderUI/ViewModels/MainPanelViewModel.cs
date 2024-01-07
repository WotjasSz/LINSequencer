using Caliburn.Micro;
using LINSequencerLib.Sequence;
using SequenceBuilderUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SequenceBuilderUI.ViewModels
{
    public class MainPanelViewModel : Conductor<Screen>.Collection.OneActive, IHandle<SequenceMessage>
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        private SequenceViewModel _sequenceView;
        private BindableCollection<SequenceModel> _activatedSequences;
        private SequenceModel _selectedSequence;
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

        public SequenceModel SelectedSequence
        {
            get { return _selectedSequence; }
            set
            {
                Set(ref _selectedSequence, value);
                _eventAggregator.PublishOnUIThreadAsync(new SequenceMessage(this, SelectedSequence));
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
            //TODO Dodać ładowanie controlki tylko w przypadku kiedy na liście jest aktywna jakaś sekwencja.
            SequenceView.ConductWith(this);
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return base.OnActivateAsync(cancellationToken);
        }

        public Task HandleAsync(SequenceMessage message, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                if (message.Sender != this)
                {
                    ActivatedSequences.Add(message.Sequence);
                    //SelectedSequence = message.Sequence;
                    
                }
            }
            );
        }
    }
}
