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
        private ObservableCollection<SequenceStepParamModel>? _inputParameters;

        [ObservableProperty]
        private ObservableCollection<SequenceStepParamModel>? _outputParameters;

        public StepParametersViewModel(SequenceStepModel sequenceStep)
        {
            SequenceStep = sequenceStep;
            UpdateSequenceField();
            WeakReferenceMessenger.Default.Register<StepParameterMessage>(this);
        }

        public void Receive(StepParameterMessage message)
        {
            SequenceStep = message.Value;
            UpdateSequenceField();
        }

        public void UpdateSequenceField()
        {
            Comment = SequenceStep.Comment;
            InputParameters = new ObservableCollection<SequenceStepParamModel>(SequenceStep.InputParameterList);
            OutputParameters = new ObservableCollection<SequenceStepParamModel>(SequenceStep.OutputParameterList);
        }

        partial void OnCommentChanged(string? value)
        {
            SequenceStep.Comment = value;
        }        
    }
}
