using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINSequencerLib.Sequence;
using Microsoft.VisualBasic.FileIO;
using SequencerUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class BoolSwitchOptionViewModel : ObservableObject
    {
        [ObservableProperty]
        private IParamOption _option;

        [ObservableProperty]
        private ObservableCollection<BoolSwitchOptionModel> _options;

        public BoolSwitchOptionViewModel(IParamOption data)
        {
            Option = data;
            if (data is ParamOptionBoolSwitch)
            {
                Options = new ObservableCollection<BoolSwitchOptionModel>();

                //Fillup options from a data string list and checking which option is selected comparing
                //index with value
                foreach (var opt in (data as ParamOptionBoolSwitch).Options.Select((value, idx) => new {value, idx}))
                {
                    BoolSwitchOptionModel bsom = new BoolSwitchOptionModel();
                    bsom.Name = opt.value;
                    bsom.Index = opt.idx;
                    bsom.IsSelected = (Option as ParamOptionBoolSwitch).Value == opt.idx ? true : false;
                    Options.Add(bsom);
                }
            }            
        }

        [RelayCommand]
        private void SelectOption(BoolSwitchOptionModel option)
        {
            (Option as ParamOptionBoolSwitch).Value = option.Index;
        }

    }
}
