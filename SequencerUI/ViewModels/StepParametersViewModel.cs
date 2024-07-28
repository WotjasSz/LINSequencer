using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using LINSequencerLib.SequenceStep;
using LINSequencerLib.SupportingFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class StepParametersViewModel : ObservableObject
    {
        [ObservableProperty]
        private SequenceStepModel? _sequenceStep;

        [ObservableProperty]
        private string? _comment;

        public StepParametersViewModel()
        {
            
        }

        public StepParametersViewModel(SequenceStepModel sequenceStep)
        {
            SequenceStep = sequenceStep;
            Comment = SequenceStep.Comment;
        }
    }
}
