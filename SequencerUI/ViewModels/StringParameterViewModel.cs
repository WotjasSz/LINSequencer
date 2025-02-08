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
        public StringParameterViewModel(SequenceStepParamModel stepParam, ObservableCollection<SequenceStepModel> stepList) 
            : base(stepParam) 
        {
            foreach (var step in stepList)
            {
                AvailableVariables.Add(step.GetStepName());
            }
            AvailableVariables.Add("data(ddMMyyyy)");
            AvailableVariables.Add("data(dddyy)");
            AvailableVariables.Add("time(HHmmss)");
        }
    }
}
