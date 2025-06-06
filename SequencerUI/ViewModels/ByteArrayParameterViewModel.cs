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
    public partial class ByteArrayParameterViewModel : ParentParameterViewModel
    {
        public ByteArrayParameterViewModel(SequenceStepParamModel stepParam, ObservableCollection<SequenceStepModel> stepList) 
            : base(stepParam) 
        {
            foreach (var step in stepList)
            {
                //TODO Rozważyć stworzenie widoku tylko dla bajtów.
                //Filtering only byte array values
                SequenceStepParamModel? outputParam = step.OutputParameterList.Where(o => o.Name == "Output").FirstOrDefault();
                ParamRawValue = StepParam.ParamValue.Replace(" ", "");
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
                string _inputType = GetInputTypeString();
                byte[] outArr;
                string outString = string.Empty;
                if (newValue.Contains('<') && newValue.Contains('>'))
                {
                    outString = newValue;
                }
                else if (newValue == string.Empty && !IsRequired)
                {
                    outString = newValue;
                }
                else
                {
                    outArr = newValue.HexStringToByteArray();
                    outString = outArr.ByteArrayToHexString();
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

        //TODO Przemyśleć sprawę walidacji wprowadzanego teksty. Być może lepiej będzie użyć Regexa do walidacji lub utworzyć metody walidacyjne w DynamiValueParser
        protected override bool IsTextValid(string inputText)
        {
            string varType = GetInputTypeString();

            if (inputText.Contains('<') && inputText.Contains('>'))
            {
                return true;
            }
            else if (inputText.All(c => "0123456789ABCDEFabcdef".Contains(c)))
            {
                return true;
            }
            else if (inputText == string.Empty && !IsRequired)
            {
                return true;
            }
            return false;
        }

        

        private byte[] HexToByteArray(string inputString)
        {
            string hexStr = inputString.Length % 2 > 0 ? string.Concat("0", inputString) : hexStr = inputString;
            byte[] outArr= Enumerable.Range(0, hexStr.Length)
                   .Where(x => x % 2 == 0)
                   .Select(x => Convert.ToByte(hexStr.Substring(x, 2), 16))
                   .ToArray();

            return outArr;
        }
    }
}