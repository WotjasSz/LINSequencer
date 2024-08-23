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
    public partial class BoolSwitchOptionViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> _options;

        [ObservableProperty]
        private int _value;

        public BoolSwitchOptionViewModel(IParamOption data)
        {
            if (data is ParamOptionBoolSwitch)
            {
                Options = new ObservableCollection<string>((data as ParamOptionBoolSwitch).Options);
                Value = (data as ParamOptionBoolSwitch).Value;
            }            
        }


    }
}
