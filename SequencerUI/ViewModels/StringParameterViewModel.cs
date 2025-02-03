using CommunityToolkit.Mvvm.ComponentModel;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class StringParameterViewModel : ParentParameterViewModel
    {
        public StringParameterViewModel(SequenceStepParamModel stepParam, ObservableCollection<string> stepList) : base(stepParam, stepList) { }
    }
}
