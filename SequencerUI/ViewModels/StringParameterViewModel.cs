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
    public partial class StringParameterViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string _paramType;

        [ObservableProperty]
        private string _paramValue;

        [ObservableProperty]
        private ObservableCollection<IParamOption> _paramOptions;

        public StringParameterViewModel(SequenceStepParamModel paramModel)
        {
            Name = paramModel.Name;
            ParamType = paramModel.ParamType;
            ParamValue = paramModel.ParamValue;
            ParamOptions = new ObservableCollection<IParamOption>(paramModel.ParamOptions);
        }

        partial void OnParamValueChanged(string value)
        {
            Debug.WriteLine(value);
        }
    }
}
