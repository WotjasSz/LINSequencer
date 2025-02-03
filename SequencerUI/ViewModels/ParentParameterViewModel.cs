using CommunityToolkit.Mvvm.ComponentModel;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SequencerUI.ViewModels
{
    public partial class ParentParameterViewModel : ObservableObject
    {
        [ObservableProperty]
        private SequenceStepParamModel _stepParam;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _paramType;

        [ObservableProperty]
        private string _paramValue;

        [ObservableProperty]
        private bool _isRequired;

        [ObservableProperty]
        private ObservableCollection<IParamOption> _paramOptions;

        [ObservableProperty]
        private ObservableCollection<string> _availableVariables;

        [ObservableProperty]
        private string _selectedVariable;

        [ObservableProperty]
        private bool _isValid;

        [ObservableProperty]
        private bool _isPopupOpen;

        public ParentParameterViewModel(SequenceStepParamModel stepParam)
        {
            StepParam = stepParam;
            Name = StepParam.Name;
            ParamType = StepParam.ParamType;
            ParamValue = StepParam.ParamValue;
            IsRequired = StepParam.IsRequired;
            ParamOptions = new ObservableCollection<IParamOption>(stepParam.ParamOptions);

            IsValid = !IsTextValid(ParamValue);

            AvailableVariables = new ObservableCollection<string> { "data(ddMMyyyy)", "data(dddyy)", "time(HHmmss)" };
        }

        public ParentParameterViewModel(SequenceStepParamModel stepParam, ObservableCollection<string> stepList)
        {
            StepParam = stepParam;
            Name = StepParam.Name;
            ParamType = StepParam.ParamType;
            ParamValue = StepParam.ParamValue;
            IsRequired = StepParam.IsRequired;
            ParamOptions = new ObservableCollection<IParamOption>(stepParam.ParamOptions);

            IsValid = !IsTextValid(ParamValue);

            AvailableVariables = new ObservableCollection<string> { "data(ddMMyyyy)", "data(dddyy)", "time(HHmmss)" };
            foreach(var step in stepList)
            {
                AvailableVariables.Add(step);
            }
        }

        protected virtual void OnValueChanged(string? oldValue, string newValue)
        {            
            ParamValue = newValue;
            StepParam.ParamValue = ParamValue;
            IsValid = !IsTextValid(ParamValue);
        }

        protected virtual void OnValueChanging(string value)
        {
            if (value != string.Empty && value[value.Length - 1] == '<')
            {
                IsPopupOpen = true;
            }
        }

        protected virtual void OnSelectedVariable(string value)
        {
            if(AvailableVariables != null)
            {
                IsPopupOpen = false;
            }
            ParamValue += $"{value}>";
        }

        protected virtual bool IsTextValid(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                return false;
            }
            return true;
        }

        partial void OnParamValueChanged(string? oldValue, string newValue)
        {
            OnValueChanged(oldValue, newValue);
        }

        partial void OnParamValueChanging(string value)
        {
                OnValueChanging(value);
        }

        partial void OnSelectedVariableChanged(string value)
        {
            OnSelectedVariable(value);
        }
    }
}
