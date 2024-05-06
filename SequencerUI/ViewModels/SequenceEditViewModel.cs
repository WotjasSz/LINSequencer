using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using LINSequencerLib.SupportingFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class SequenceEditViewModel : ObservableObject
    {
        [ObservableProperty]
        private SequenceModel _sequence;

        [ObservableProperty]
        private ObservableCollection<SdfFileModel> _sdfFiles;

        [ObservableProperty]
        private ObservableCollection<SeqFunction> _functionList;

        [ObservableProperty]
        private SeqFunction _selectedFunction;

        public SequenceEditViewModel()
        {
            Sequence = new SequenceModel();
            SdfFiles = new ObservableCollection<SdfFileModel>(LinSequencer.SdfList);
            FunctionList = new ObservableCollection<SeqFunction>(LinSequencer.FunctionList);
        }

        public SequenceEditViewModel(SequenceModel sequence)
        {
            Sequence = sequence;
            SdfFiles = new ObservableCollection<SdfFileModel>(LinSequencer.SdfList);
            FunctionList = new ObservableCollection<SeqFunction>(LinSequencer.FunctionList);
        }

        [RelayCommand]
        private void SaveSequence()
        {
            LinSequencer.SaveSequence(Sequence);
        }

        [RelayCommand]
        private void AddSequence()
        {
            Sequence.StepList.Add(new SequenceStepModel(Sequence.StepList.Count, SelectedFunction));
        }
    }
}
