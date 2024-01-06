using Caliburn.Micro;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SequenceBuilderUI.ViewModels
{
    public class AvailableFunctionViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private IWindowManager _windowManager;

        private BindableCollection<SeqFunction> _sequenceFunction;

        public BindableCollection<SeqFunction> SequenceFunction
        {
            get { return _sequenceFunction; }
            set 
            { 
                Set(ref _sequenceFunction, value); 
            }
        }


        public AvailableFunctionViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => SequenceFunction = new BindableCollection<SeqFunction>(LinSequencer.FunctionList));
        }
    }
}
