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
        private SeqFunction _selectedFunction;

        public BindableCollection<SeqFunction> SequenceFunction
        {
            get { return _sequenceFunction; }
            set 
            { 
                Set(ref _sequenceFunction, value); 
            }
        }       

        public SeqFunction SelectedFunction
        {
            get { return _selectedFunction; }
            set 
            {
                Set(ref _selectedFunction, value);
            }
        }

        public AvailableFunctionViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
        }

        public void DragSelectedFunction(EventArgs eventArgs)
        {
            if (_selectedFunction == null) { return; }
            Console.WriteLine("Jest git!!!");
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => SequenceFunction = new BindableCollection<SeqFunction>(LinSequencer.FunctionList));
        }
    }
}
