﻿using CommunityToolkit.Mvvm.ComponentModel;
using LINSequencerLib.Sequence;
using LINSequencerLib.SupportingFiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SequencerUI.ViewModels
{
    public partial class ByteParameterViewModel : ParentParameterViewModel
    {
        public ByteParameterViewModel(SequenceStepParamModel stepParam, ObservableCollection<SequenceStepModel> stepList) 
            : base(stepParam) 
        {
            foreach (var step in stepList)
            {
                //TODO Rozważyć stworzenie widoku tylko dla bajtów.
                //Filtering only byte array values
                SequenceStepParamModel? outputParam = step.OutputParameterList.FirstOrDefault(o => o.Name == "Output");
                InitParamRawValueSet(StepParam.ParamValue);
                if (outputParam != null && outputParam.ParamType.Equals("System.Byte"))
                {
                    AvailableVariables.Add(step.GetStepName());
                }
            }
        }

        protected override void OnValueChanged(string? oldValue, string newValue)
        {
            string _inputType = GetInputTypeString();
            if (IsTextValid(newValue))
            {
                byte[] outArr;
                string outString = string.Empty;
                if (newValue.StartsWith('<') && newValue.EndsWith('>'))
                {
                    outString = newValue;
                }
                else if (newValue == string.Empty && !IsRequired)
                {
                    outString = newValue;
                }
                else if (_inputType == "HEX")
                {                    
                    outString = newValue;
                }
                else if (_inputType == "DEC")
                {   
                    outString = $"{int.Parse(newValue):X2}";
                }

                ParamValue = outString;
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
            string _inputType = GetInputTypeString();
            if (inputText.StartsWith('<') && inputText.EndsWith('>'))
            {
                return true;
            }
            else if (_inputType == "HEX" && inputText.Length <= 2 && inputText.All(c => "0123456789ABCDEFabcdef".Contains(c)))
            {
                return true;
            }
            else if (_inputType == "DEC" && inputText.All(char.IsDigit))
            {                
                if (int.TryParse(inputText, out int result) && result >= byte.MinValue && result <= byte.MaxValue)
                {
                    return true;
                }
                return false;
            }
            else if (inputText == string.Empty && !IsRequired)
            {
                return true;
            }
            return false;
        }

        private void InitParamRawValueSet(string value)
        {
            string _inputType = GetInputTypeString();
            if (_inputType == "DEC")
            {
                SetParamRawValue(Convert.ToInt32(value, 16).ToString());
            }
        }
    }
}