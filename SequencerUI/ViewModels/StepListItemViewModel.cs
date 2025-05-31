using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class StepListItemViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;

        [ObservableProperty]
        private SequenceStepModel? _sequenceStep;

        [ObservableProperty]
        private int _index;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _userStepName;

        public StepListItemViewModel(SequenceStepModel sequenceStep)
        {
            SequenceStep = sequenceStep;
            Index = sequenceStep.Index;
            Name = sequenceStep.Name;
            Description = sequenceStep.Comment;
            UserStepName = sequenceStep.GetStepName();
        }
    }
}
