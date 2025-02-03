using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class IntParameterViewModel : ParentParameterViewModel
    {   
        public IntParameterViewModel(SequenceStepParamModel stepParam, ObservableCollection<string> stepList) : base(stepParam, stepList)
        {
            
        }
    }
}
