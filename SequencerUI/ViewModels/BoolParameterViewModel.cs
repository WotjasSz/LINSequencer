﻿using CommunityToolkit.Mvvm.ComponentModel;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{   
    public partial class BoolParameterViewModel : ParentParameterViewModel
    {
        public BoolParameterViewModel(SequenceStepParamModel stepParam) : base(stepParam) { }
    }
}
