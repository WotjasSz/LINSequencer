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
    public partial class ByteParameterViewModel : ParentParameterViewModel
    {
        public ByteParameterViewModel(SequenceStepParamModel stepParam, ObservableCollection<SequenceStepModel> stepList) 
            : base(stepParam) 
        {
            foreach (var step in stepList)
            {
                //Filtering only int values
                SequenceStepParamModel? outputParam = step.OutputParameterList.Where(o => o.Name == "Output").FirstOrDefault();
                if (outputParam != null && outputParam.ParamType.Equals("System.Byte[]"))
                {
                    AvailableVariables.Add(step.GetStepName());
                }
            }
        }

        protected override void OnValueChanged(string? oldValue, string newValue)
        {
            if (IsTextValid(newValue))
            {
                ParamValue = newValue;
                StepParam.ParamValue = ParamValue;
                IsInvalid = false;
            }
            else
            {
                IsInvalid = true;
            }
        }

        protected override bool IsTextValid(string inputText)
        {
            if (inputText.StartsWith('<') && inputText.EndsWith('>'))
            {
                return true;
            }
            else if(inputText.All(c => "0123456789ABCDEFabcdef".Contains(c)))
            {
                return true;
            }
            return false;
        }
    }
}