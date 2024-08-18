using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using SequencerUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SequencerUI.ViewModels
{
    //TODO Add ScrollView style
    public partial class MainWindowViewModel : ObservableRecipient
    {
        #region Properties
        [ObservableProperty]
        private string? _title = "Sequencer";

        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        private bool _isAddSeqPanelEnable;

        [ObservableProperty]
        private bool _isPanelButtonEnabled = true;

        [ObservableProperty]
        private ObservableCollection<SequenceModel>? _availableSequences;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddSequenceCommand))]
        private SequenceModel? _availableSeqSelected;

        [ObservableProperty]
        private ObservableCollection<SequenceModel>? _activeSequences;

        [ObservableProperty]
        private SequenceModel? _currentSequence;

        private SequenceRunView _seqRunView;
        private SequenceEditView _sequenceEditView;
        private AboutView _aboutView;

        private bool _editModeAvailable = true;

        #endregion


        public MainWindowViewModel()
        {
            LinSequencer.InitializeLinSequencer();

            AvailableSequences = new ObservableCollection<SequenceModel>(LinSequencer.SequenceList);
            ActiveSequences = new ObservableCollection<SequenceModel>();

            _seqRunView = new SequenceRunView();
            _sequenceEditView = new SequenceEditView();
            _aboutView = new AboutView();

            _aboutView.DataContext = new AboutViewModel();
            CurrentView = _aboutView;   
        }

        #region Property actions
        partial void OnCurrentSequenceChanged(SequenceModel? value)
        {
            //TODO zmienić to na tworzenie jednego obiektu i update  sequence a nie za każdym razem tworzenie od nowa
            // Być moze użycie AutoFac??            
            _seqRunView.DataContext = new SequenceRunViewModel(value);
            CurrentView = _seqRunView;
        }
        #endregion

        #region Commands

        [RelayCommand]
        private void ShowAboutControl()
        {
            CurrentSequence = null;
            CurrentView = new AboutView() { DataContext = new AboutViewModel() };
        }

        [RelayCommand(CanExecute = nameof(CanExeuteAddSequence))]
        private void AddSequence()
        {
            if (AvailableSeqSelected != null)
            {
                ActiveSequences.Add(AvailableSeqSelected);
                AvailableSeqSelected = null;
            }
            IsAddSeqPanelEnable = !IsAddSeqPanelEnable;
            IsPanelButtonEnabled = !IsAddSeqPanelEnable;
        }
        [RelayCommand]
        private void AvailableSequenceReload()
        {
            AvailableSequences = null;
            LinSequencer.ReloadAvailableSequences();
            AvailableSequences = new ObservableCollection<SequenceModel>(LinSequencer.SequenceList);
        }

        [RelayCommand]
        private void AddNewSequence()
        {
            //Debug.WriteLine("Dodawanie nowej sekwencji...");
            SequenceModel sequenceModel = new SequenceModel();
            ActiveSequences.Add(sequenceModel);
            CurrentSequence = sequenceModel;
            _sequenceEditView.DataContext = new SequenceEditViewModel(sequenceModel);
            CurrentView = _sequenceEditView;
            IsAddSeqPanelEnable = !IsAddSeqPanelEnable;
            IsPanelButtonEnabled = !IsAddSeqPanelEnable;
        }

        [RelayCommand]
        private void RemoveSequence(SequenceModel seq)
        {
            if (CurrentSequence == seq)
            {
                CurrentView = null;
            }
            ActiveSequences.Remove(seq);
        }

        [RelayCommand(CanExecute = nameof(CanExecuteEditSequence))]
        private void EditSequence(SequenceModel seq)
        {
            _sequenceEditView.DataContext = new SequenceEditViewModel(seq);
            CurrentView = _sequenceEditView;
        }

        private bool CanExeuteAddSequence()
        {
            return AvailableSeqSelected != null;
        }

        private bool CanExecuteEditSequence()
        {
            return _editModeAvailable;
        }

        [RelayCommand]
        private void ToggleSeqPanel()
        {
            IsAddSeqPanelEnable = !IsAddSeqPanelEnable;
            IsPanelButtonEnabled = !IsAddSeqPanelEnable;
        }

        #endregion

        #region Window actions

        [RelayCommand]
        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        [RelayCommand]
        private void MaximizeWindow()
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        [RelayCommand]
        private void CloseWidnow()
        {
            Application.Current.Shutdown();
        }     

        #endregion

    }
}
