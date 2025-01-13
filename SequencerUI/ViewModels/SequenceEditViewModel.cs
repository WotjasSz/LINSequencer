using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GongSolutions.Wpf.DragDrop;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using LINSequencerLib.SequenceStep;
using LINSequencerLib.SupportingFiles;
using SequencerUI.Helpers;
using SequencerUI.Services;
using SequencerUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class SequenceEditViewModel : ObservableRecipient, IDropTarget
    {
        [ObservableProperty]
        private bool _isFunctionListVisible;

        [ObservableProperty]
        private bool _isExpandButtonVisible = true;

        [ObservableProperty]
        private DateTime _creationDate;

        [ObservableProperty]
        private string _fileName;

        [ObservableProperty]
        private SequenceModel _sequence;

        [ObservableProperty]
        private SequenceModel _sequenceCopy;

        [ObservableProperty]
        private object? _currentStepParamView;

        [ObservableProperty]
        private ObservableCollection<SdfFileModel> _sdfFiles;

        [ObservableProperty]
        private SdfFileModel? _selectedSdfFile;

        [ObservableProperty]
        private ObservableCollection<SeqFunction>? _functionList;

        [ObservableProperty]        
        private SeqFunction? _selectedFunction;

        [ObservableProperty]
        private ObservableCollection<SequenceStepModel> _stepList;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(StepList))]
        private SequenceStepModel? _selectedSeqenceStep;

        private StepParametersView _stepParametersView;

        private readonly IMessenger _messenger;

        public SequenceEditViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            Sequence = new SequenceModel();
            StepList = new ObservableCollection<SequenceStepModel>(Sequence.StepList);
            SdfFiles = new ObservableCollection<SdfFileModel>(LinSequencer.SdfList);

            CreationDate = Sequence.CreationDate;
            FileName = Sequence.FileName;

            _stepParametersView = new StepParametersView();

            LoadFunctionList();
        }

        public SequenceEditViewModel(IMessenger messenger, SequenceModel sequence)
        {
            _messenger = messenger;
            Sequence = sequence;
            SequenceCopy = sequence.DeepCloneJson(); //Needed for cancelation
            StepList = new ObservableCollection<SequenceStepModel>(Sequence.StepList);
            SdfFiles = new ObservableCollection<SdfFileModel>(LinSequencer.SdfList);
            SelectedSdfFile = SdfFiles.Where(p => p.Name == sequence.SdfName).FirstOrDefault();

            CreationDate = Sequence.CreationDate;
            FileName = Sequence.FileName;

            _stepParametersView = new StepParametersView();

            LoadFunctionList();
        }

        //TODO dodać kopię sekwencji podczas edytowania aby po naciśnięciu cancel można było wrócić do wartości oryginalnych
        //TODO dodać możliwość porównywania obiektu oryginalnego ze zmodyfikowanym w celu np. wyświetleniu monitu o potrzebie zapisania, itp
        //Do tego celu najlepiej nadpisać metody GetHashCode oraz Equal wykorzystując bibliotekę Equatable

        //TODO zmodyfikować widok listy kroków tak aby wyświetlał nazwę nadaną przez użytkownika a nazwa funkcji była z małych liter
        //Trzeba do tego zmodyfikować metode SequenceStepModel.

        #region Property actions
        partial void OnSelectedSeqenceStepChanged(SequenceStepModel? value)
        {
            if (SelectedSeqenceStep != null)
            {
                _stepParametersView.DataContext = new StepParametersViewModel(SelectedSeqenceStep);
                CurrentStepParamView = _stepParametersView;
            }
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
        private void SaveSequenceAs()
        {            
            Sequence.StepList.Clear();
            Sequence.StepList = StepList.ToList();
            Sequence.UpdateFileName();
            Sequence.SdfName = SelectedSdfFile.Name;

            CreationDate = Sequence.CreationDate;
            FileName = Sequence.FileName;

            LinSequencer.SaveSequence(Sequence);
        }

        [RelayCommand]
        private void CancelEdition()
        {
            //TODO Zmienić sposób kopiowania tak aby nadpisać dane dla kokretnej referencji a nie zmienić referencję.
            //TODO Dodać metodę w klasie SequenceModel do nadpisywania wartości.
            //Sequence = SequenceCopy.DeepCloneJson();

            ViewMessage message = new ViewMessage(EViewMode.RunMode, Sequence);
            _messenger.Send(new GenericMessage<ViewMessage>(message));
        }

        [RelayCommand]
        private void SaveSequenceAndTest()
        {
            Sequence.StepList.Clear();
            Sequence.StepList = StepList.ToList();
            Sequence.SdfName = SelectedSdfFile.Name;
            LinSequencer.SaveSequence(Sequence);

            ViewMessage message = new ViewMessage(EViewMode.RunMode, Sequence);
            _messenger.Send(new GenericMessage<ViewMessage>(message));
        }

        [RelayCommand]
        private void AddSeqStep()
        {
            if(SelectedFunction != null)
            {
                StepList.Add(new SequenceStepModel(StepList.Count, SelectedFunction));
                UpdateIndex();
            }            
        }

        [RelayCommand]
        private void DuplicateStep(SequenceStepModel step)
        {
            var newStep = step.DeepCloneJson();
            newStep.Index = StepList.Count + 1;
            StepList.Add(newStep);
        }

        [RelayCommand]
        private void RemoveStep(SequenceStepModel step)
        {            
            if (StepList.Contains(step))
            {
                StepList.Remove(step);
                SelectedSeqenceStep = null;
                CurrentStepParamView = null;
                UpdateIndex();
            }
        }

        [RelayCommand]
        private void ToggleFunctionListVisibility()
        {
            IsExpandButtonVisible = !IsExpandButtonVisible;
            IsFunctionListVisible = !IsFunctionListVisible;
        }
        #endregion

        #region DragDrop
        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.Effects = System.Windows.DragDropEffects.Move;            
            dropInfo.NotHandled = false;
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is SequenceStepModel sourceStep && dropInfo.TargetItem is SequenceStepModel targetStep)
            {                
                int sourceIndex = StepList.IndexOf(sourceStep);
                int targetIndex = StepList.IndexOf(targetStep);

                if (sourceIndex != targetIndex)
                {
                    StepList.Move(sourceIndex, targetIndex);
                }
            }
            else if(dropInfo.Data is SeqFunction function)
            {                
                int targetIndex = dropInfo.InsertIndex;

                if (StepList != null)
                {
                    StepList.Insert(targetIndex, new SequenceStepModel(targetIndex + 1, function));                    
                }
            }

            UpdateIndex();
        }
        #endregion

        #region Functions
        private void LoadFunctionList()
        {
            FunctionList = new ObservableCollection<SeqFunction>(LinSequencer.FunctionList);
        }

        private void UpdateIndex()
        {
            for (int i = 0; i < StepList.Count; i++)
            {
                StepList[i].Index = i;
            }
            Sequence.StepList = StepList.ToList();
            StepList.Clear();
            StepList = new ObservableCollection<SequenceStepModel>(Sequence.StepList);
        }
        #endregion
    }
}
