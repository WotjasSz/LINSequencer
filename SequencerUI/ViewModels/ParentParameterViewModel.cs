﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib.Sequence;
using SequencerUI.Helpers;
using SequencerUI.Resources.Controls;
using SequencerUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SequencerUI.ViewModels
{
    public partial class ParentParameterViewModel : ObservableRecipient
    {
        private readonly IMessenger _messenger;

        [ObservableProperty]
        private SequenceStepParamModel _stepParam;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _paramType;

        [ObservableProperty]
        private string _paramValue;
        
        [ObservableProperty]
        private string _paramRawValue;

        [ObservableProperty]
        private bool _isRequired;

        [ObservableProperty]
        private ObservableCollection<IParamOption> _paramOptions;

        [ObservableProperty]
        private ObservableCollection<string> _availableVariables;

        [ObservableProperty]
        private string _selectedVariable;

        [ObservableProperty]
        private bool _isInvalid;

        [ObservableProperty]
        private bool _isPopupOpen;

        //TODO Add additional field for converted variables like dec and hex or byte array
        //TODO Add addiitonal viewmodel for complex and simple string field or only simple text option

        public ParentParameterViewModel(SequenceStepParamModel stepParam)
        {
            _messenger = ServiceLocator.GetService<IMessenger>();
            _messenger.Register<GenericMessage<ControlMessage>>(this, (r, m) =>
            {
                HandleReceiveMessage(r, m);
            });

            StepParam = stepParam;
            Name = StepParam.Name;
            ParamOptions = new ObservableCollection<IParamOption>(StepParam.ParamOptions);
            ParamValue = StepParam.ParamValue;
            SetParamRawValue(StepParam.ParamValue);  //Set raw value to avoid calling OnValueChanged during initialization
            IsRequired = StepParam.IsRequired;            

            //Split full type name to get only type name
            string[] typeParts = StepParam.ParamType.Split('.');
            ParamType = typeParts[^1];

            IsInvalid = !IsTextValid(ParamRawValue) && IsRequired;

            AvailableVariables = new ObservableCollection<string>();
        }

        #region On properties change
        protected virtual void OnValueChanged(string? oldValue, string newValue)
        {
            ParamValue = newValue;
            StepParam.ParamValue = ParamValue;
            IsInvalid = !IsTextValid(ParamValue) && IsRequired;
        }

        protected virtual void OnSelectedVariable(string value)
        {
            if (AvailableVariables != null)
            {
                IsPopupOpen = false;
            }
            ParamRawValue += $"{value}>";
        }

        protected virtual bool IsTextValid(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Set raw value of the parameter.
        /// Remark: Should be used only durign initialization to avoid calling OnValueChanged
        /// </summary>
        /// <param name="value"></param>
        protected void SetParamRawValue(string value)
        {
            _paramRawValue = value;  //Use direct field assigment to avoid calling OnParamRawValueChanged during initialization
        }

        protected virtual string GetInputTypeString()
        {
            if (ParamOptions == null)
            {
                return string.Empty;
            }
            var inputType = ParamOptions.FirstOrDefault(t => t.Name == "InputType");
            if (inputType != null && inputType is ParamOptionBoolSwitch)
            {
                return ((ParamOptionBoolSwitch)inputType).GetSelectedOptionString();
            }
            return string.Empty;
        }

        partial void OnParamRawValueChanged(string? oldValue, string newValue)
        {
            OnValueChanged(oldValue, newValue);
        }

        partial void OnSelectedVariableChanged(string value)
        {
            OnSelectedVariable(value);
        } 
        #endregion

        private void HandleReceiveMessage(object r, GenericMessage<ControlMessage> m)
        {
            if ((m.Content.ControlInstance as CustomTextBox).CustomName == Name)
            {
                switch (m.Content.CtrlMessage)
                {
                    case EControlMessage.ShowPopup:
                        IsPopupOpen = true;
                        break;
                    case EControlMessage.HidePopup:
                        IsPopupOpen = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
