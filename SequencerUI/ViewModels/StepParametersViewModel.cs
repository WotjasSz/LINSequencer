using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using LINSequencerLib.SequenceStep;
using LINSequencerLib.SupportingFiles;
using SequencerUI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class StepParametersViewModel : ObservableRecipient, IRecipient<StepParameterMessage>
    {
        [ObservableProperty]
        private SequenceStepModel _sequenceStep;

        [ObservableProperty]
        private string? _comment;

        [ObservableProperty]
        private ObservableCollection<object>? _inputParametersVm;

        [ObservableProperty]
        private ObservableCollection<object>? _outputParametersVm;

        [ObservableProperty]
        private bool _isInputParamAvailable;

        [ObservableProperty]
        private bool _isOutputParamAvailable;

        private ObservableCollection<SequenceStepModel> _variablesList;

        public StepParametersViewModel(SequenceStepModel sequenceStep, ObservableCollection<SequenceStepModel> stepList)
        {
            SequenceStep = sequenceStep;
            Comment = sequenceStep.Comment;
            InputParametersVm = new ObservableCollection<object>();
            OutputParametersVm = new ObservableCollection<object>();            
            //WeakReferenceMessenger.Default.Register<StepParameterMessage>(this);

            _variablesList = new ObservableCollection<SequenceStepModel>(stepList.Where(s => s.Index < sequenceStep.Index).ToList());

            UpdateSequenceField();
        }

        public void Receive(StepParameterMessage message)
        {
            SequenceStep = message.Value;
            UpdateSequenceField();
        }

        public void UpdateSequenceField()
        {
            foreach (var parameter in SequenceStep.InputParameterList)
            {
                if (parameter.ParamType.Equals("System.Boolean"))
                {
                    InputParametersVm.Add(new BoolParameterViewModel(parameter));
                }
                else if (parameter.ParamType.Equals("System.String"))
                {
                    InputParametersVm.Add(new StringParameterViewModel(parameter, _variablesList));
                }
                else if (parameter.ParamType.Equals("System.Int32"))
                {
                    InputParametersVm.Add(new IntParameterViewModel(parameter, _variablesList));
                }
                //else if (parameter.ParamType.Equals("System.Byte[]"))
                //{
                //    InputParametersVm.Add(new ByteParameterViewModel(parameter)); //Temporarly later add ArrayControl
                //}
                else
                {
                    InputParametersVm.Add(new StringParameterViewModel(parameter, _variablesList));
                }
            }

            //TODO Add list with output parameters
            if (InputParametersVm.Count > 0) {IsInputParamAvailable = true;}
            else {IsInputParamAvailable = false;}

            if (_outputParametersVm.Count > 0) {IsOutputParamAvailable = true;}
            else{IsOutputParamAvailable = false;}
        }
    }
}
