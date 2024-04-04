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
    public partial class MainWindowViewModel : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        private string? _title = "Sequencer";

        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        private bool _isAddSeqVisible;

        [ObservableProperty]
        private ObservableCollection<SequenceModel>? _availableSequences;
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddSequenceCommand))]
        private SequenceModel? _availableSeqSelected;

        [ObservableProperty]
        private ObservableCollection<SequenceModel>? _activeSequences;

        #endregion


        public MainWindowViewModel()
        {
            //AddSequenceCommand = new RelayCommand(AddSequence);

            LinSequencer.InitializeLinSequencer();

            _availableSequences = new ObservableCollection<SequenceModel>(LinSequencer.SequenceList);
            _activeSequences = new ObservableCollection<SequenceModel>();
        }

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
            IsAddSeqVisible = !IsAddSeqVisible;
        }

        [RelayCommand]
        private void AddNewSequence()
        {
            Debug.WriteLine("Tymczosowo");
            IsAddSeqVisible = !IsAddSeqVisible;
        }

        private bool CanExeuteAddSequence()
        {
            return AvailableSeqSelected != null;
        }

        [RelayCommand]
        private void ToggleAddSeqVisibility()
        {
            IsAddSeqVisible = !IsAddSeqVisible;
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
