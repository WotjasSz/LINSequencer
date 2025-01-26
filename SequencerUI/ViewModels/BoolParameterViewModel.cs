using CommunityToolkit.Mvvm.ComponentModel;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{   
    public partial class BoolParameterViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string _paramType;

        [ObservableProperty]
        private string _paramValue;

        [ObservableProperty]
        private ObservableCollection<IParamOption> _paramOptions;


        public BoolParameterViewModel(SequenceStepParamModel paramModel)
        {
            Name = paramModel.Name;
            ParamType = paramModel.ParamType;
            ParamValue = paramModel.ParamValue;
            ParamOptions = new ObservableCollection<IParamOption>(paramModel.ParamOptions);
        }
    }
}
