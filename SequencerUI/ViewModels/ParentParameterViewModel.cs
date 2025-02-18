using CommunityToolkit.Mvvm.ComponentModel;
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
            ParamType = StepParam.ParamType;            
            ParamValue = StepParam.ParamValue;
            ParamRawValue = StepParam.ParamValue.Replace(" ", "");
            IsRequired = StepParam.IsRequired;
            ParamOptions = new ObservableCollection<IParamOption>(stepParam.ParamOptions);

            IsInvalid = !IsTextValid(ParamValue) && IsRequired;

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

        protected virtual string GetInputTypeString()
        {
            if (ParamOptions == null)
            {
                return string.Empty;
            }
            var inputType = ParamOptions.Where(t => t.Name == "InputType").FirstOrDefault();
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
