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
    public class PropertiesViewModel : Screen, IHandle<SequenceStepModel>
    {
        private IEventAggregator _eventAggregator;
        private IWindowManager _windowManager;

        private SequenceStepModel _stepProperties;
        private BindableCollection<Dictionary<string, string>> _parameters;

        public SequenceStepModel StepProperties
        {
            get { return _stepProperties; }
            set 
            { 
                Set(ref _stepProperties, value); 
            }
        }       

        public BindableCollection<Dictionary<string,string>> Parameters
        {
            get { return _parameters; }
            set 
            { 
                Set(ref _parameters, value); 
            }
        }

        public PropertiesViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
        }

        public Task HandleAsync(SequenceStepModel message, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _stepProperties = message;
                //Parameters = new BindableCollection<Dictionary<string, string>>(message.ParameterList);
            });
        }
    }
}
