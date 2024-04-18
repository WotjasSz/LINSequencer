using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class MainWindowViewModel : ObservableObject
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

        #endregion


        public MainWindowViewModel()
        {
            LinSequencer.InitializeLinSequencer();

            _availableSequences = new ObservableCollection<SequenceModel>(LinSequencer.SequenceList);
            _activeSequences = new ObservableCollection<SequenceModel>();
        }

        #region Property actions
        partial void OnCurrentSequenceChanged(SequenceModel? value)
        {
            CurrentView = new SequenceRun() { DataContext = new SequenceRunViewModel(value) };
        }
        #endregion

        #region Commands

        [RelayCommand]
        private void ShowAboutControl()
        {
            CurrentView = new AboutView() { DataContext = new AboutViewModel() };
        }

        [RelayCommand(CanExecute = nameof(CanExeuteAddSequence))]
        private void AddSequence()
        {
            ActiveSequences.Add(AvailableSeqSelected);
            IsAddSeqPanelEnable = !IsAddSeqPanelEnable;
            IsPanelButtonEnabled = !IsAddSeqPanelEnable;
        }

        [RelayCommand]
        private void AddNewSequence()
        {
            Debug.WriteLine("Tymczasowo");
            IsAddSeqPanelEnable = !IsAddSeqPanelEnable;
            IsPanelButtonEnabled = !IsAddSeqPanelEnable;
        }

        private bool CanExeuteAddSequence()
        {
            return AvailableSeqSelected != null;
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
