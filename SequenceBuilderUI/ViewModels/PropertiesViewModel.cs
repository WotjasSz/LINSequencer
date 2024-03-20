using Caliburn.Micro;
using LINSequencerLib.Sequence;
using SequenceBuilderUI.Models;
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

        private string _name;
        private string _comment;

        private SequenceStepModel _seqStep;
        private BindableCollection<ParameterModel> _inputParameters;
        private BindableCollection<ParameterModel> _outputParameters;

        public string Name
        {
            get { return _name; }
            private set 
            { 
                Set(ref _name, value);
            }
        }

        public string Comment
        {
            get { return _comment; }
            set 
            {
                Set(ref _comment, value);
                SeqStep.Comment = value;
            }
        }

        public SequenceStepModel SeqStep
        {
            get { return _seqStep; }
            set
            {
                Set(ref _seqStep, value);
            }
        }

        public BindableCollection<ParameterModel> InputParameters
        {
            get { return _inputParameters; }
            set 
            { 
                Set(ref _inputParameters, value);
            }
        }

        public BindableCollection<ParameterModel> OutputParameters
        {
            get { return _outputParameters; }
            set
            {
                Set(ref _outputParameters, value);
            }
        }

        public PropertiesViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;

            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        public Task HandleAsync(SequenceStepModel message, CancellationToken cancellationToken)
        {

            return Task.Run(() =>
            {
                _seqStep = message;
                Name = message.Name;
                Comment = message.Comment;
                InputParameters = new BindableCollection<ParameterModel>();
                foreach (KeyValuePair<string, string> pair in message.InputParameterList)
                {
                    InputParameters.Add(new ParameterModel(pair.Key, pair.Value));
                }
            });
        }
    }
}
