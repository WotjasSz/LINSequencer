using CommunityToolkit.Mvvm.ComponentModel;
using LINSequencerLib.Sequence;
using SequencerUI.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class SequenceStepViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        SequenceStepModel _sequenceStep;

        //public SequenceStepViewModel()
        //{
        //    Debug.WriteLine("OK i po inicjalizacji");
        //    SequenceStep = new SequenceStepModel(0, "");
        //}

        //public SequenceStepViewModel(SequenceStepModel model)
        //{
        //    SequenceStep = model;
        //}
    }
}
