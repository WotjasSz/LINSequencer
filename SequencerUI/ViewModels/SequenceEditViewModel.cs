using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using LINSequencerLib.SequenceStep;
using LINSequencerLib.SupportingFiles;
using SequencerUI.Helpers;
using SequencerUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class SequenceEditViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private SequenceModel _sequence;

        [ObservableProperty]
        private object _currentStepParamView;

        [ObservableProperty]
        private ObservableCollection<SdfFileModel> _sdfFiles;

        [ObservableProperty]
        private SdfFileModel? _selectedSdfFile;

        [ObservableProperty]
        //private ObservableGroupedCollection<string, SeqFunction> _functionList;
        private ObservableCollection<SeqFunction> _functionList;

        [ObservableProperty]        
        private SeqFunction? _selectedFunction;

        [ObservableProperty]
        private ObservableCollection<SequenceStepModel> _stepList;

        [ObservableProperty]        
        private SequenceStepModel? _selectedSeqenceStep;

        private StepParametersView _stepParametersView;

        public SequenceEditViewModel()
        {
            Sequence = new SequenceModel();
            StepList = new ObservableCollection<SequenceStepModel>(Sequence.StepList);
            SdfFiles = new ObservableCollection<SdfFileModel>(LinSequencer.SdfList);
            _stepParametersView = new StepParametersView();
            LoadFunctionList();
        }

        public SequenceEditViewModel(SequenceModel sequence)
        {
            Sequence = sequence;
            StepList = new ObservableCollection<SequenceStepModel>(Sequence.StepList);
            SdfFiles = new ObservableCollection<SdfFileModel>(LinSequencer.SdfList);
            _stepParametersView = new StepParametersView();
            LoadFunctionList();
        }

        #region Property actions
        partial void OnSelectedSeqenceStepChanged(SequenceStepModel? value)
        {
            if(CurrentStepParamView == null)
            {
                _stepParametersView.DataContext = new StepParametersViewModel(value);
                CurrentStepParamView = _stepParametersView;
            }

            WeakReferenceMessenger.Default.Send(new StepParameterMessage(value));
        }
        #endregion

        #region Commands
        [RelayCommand]
        private void SaveSequence()
        {
            Sequence.StepList.Clear();
            Sequence.StepList = StepList.ToList();
            Sequence.SdfName = SelectedSdfFile.Name;
            LinSequencer.SaveSequence(Sequence);
        }

        [RelayCommand]
        private void AddSeqStep()
        {
            if(SelectedFunction != null)
            {
                StepList.Add(new SequenceStepModel(StepList.Count, SelectedFunction));
            }            
        }

        private void LoadFunctionList()
        {
            FunctionList = new ObservableCollection<SeqFunction>(LinSequencer.FunctionList);
        }
        #endregion
    }
}
