using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class IntParameterViewModel : ParentParameterViewModel
    {   
        public IntParameterViewModel(SequenceStepParamModel stepParam, ObservableCollection<SequenceStepModel> stepList)
            : base(stepParam)
        {
            foreach (var step in stepList)
            {
                //Filtering only int values
                SequenceStepParamModel? outputParam = step.OutputParameterList.Where(o => o.Name == "Output").FirstOrDefault();
                if (outputParam != null && outputParam.ParamType.Equals("System.Int32"))
                {
                    AvailableVariables.Add(step.GetStepName());
                }
            }
        }

        protected override void OnValueChanged(string? oldValue, string newValue)
        {
            if(IsTextValid(newValue))
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
            else if (int.TryParse(inputText, out int decVal))
            {
                return true;
            }
            else if (int.TryParse(inputText, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hexVal))
            {
                return true; //TODO if needed?
            }
            else
            {
                return false;
            }
        }
    }
}
