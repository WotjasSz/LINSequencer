using Caliburn.Micro;
using LINSequencerLib.Sequence;
using SequenceBuilderUI.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SequenceBuilderUI.ViewModels
{
    public class PropertiesViewModel : Screen, IHandle<SequenceStepModel>
    {
        #region Fields
        private IEventAggregator _eventAggregator;
        private IWindowManager _windowManager;

        private string _name;
        private string _comment;

        private SequenceStepModel _seqStep;
        private BindableCollection<ParameterModel> _inputParameters;
        private BindableCollection<ParameterModel> _outputParameters;
        private ParameterModel _selectedInputParameter;
        private ParameterModel _selectedOutputParameter;
        #endregion

        #region Properties
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

        public ParameterModel SelectedInputParameter
        {
            get { return _selectedInputParameter; }
            set
            {
                Set(ref _selectedInputParameter, value);
            }
        }

        public ParameterModel SelectedOutputParameter
        {
            get { return _selectedOutputParameter; }
            set
            {
                Set(ref _selectedOutputParameter, value);
            }
        }

        public void Confirm()
        {
            _seqStep.Comment = Comment;
            foreach (ParameterModel param in InputParameters)
            {
                _seqStep.InputParameterList[param.Name] = param.Value;
            }
            foreach (ParameterModel param in OutputParameters)
            {
                _seqStep.OutputParameterList[param.Name] = param.Value;
            }

            _eventAggregator.PublishOnUIThreadAsync(new ParameterMessage(this, _seqStep));
        }

        #endregion


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
                OutputParameters = new BindableCollection<ParameterModel>();
                foreach (KeyValuePair<string, string> pair in message.InputParameterList)
                {
                    InputParameters.Add(new ParameterModel(EParameterType.Input, pair.Key, pair.Value));
                }
                foreach (KeyValuePair<string, string> pair in message.OutputParameterList)
                {
                    OutputParameters.Add(new ParameterModel(EParameterType.Output, pair.Key, pair.Value));
                }
            });
        }
    }
}
