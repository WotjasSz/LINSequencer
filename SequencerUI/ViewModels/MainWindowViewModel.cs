using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using SequencerUI.Services;
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
        private object _navigationContentView;

        private SequenceRunView _seqRunView;
        private SequenceEditView _sequenceEditView;
        private AboutView _aboutView;
        private SequenceSelectionView _sequenceSelectionView;

        #endregion


        public MainWindowViewModel()
        {
            LinSequencer.InitializeLinSequencer();           

            _seqRunView = new SequenceRunView();
            _sequenceEditView = new SequenceEditView();
            _aboutView = new AboutView();
            _sequenceSelectionView = new SequenceSelectionView();

            _aboutView.DataContext = new AboutViewModel();
            _sequenceSelectionView.DataContext = new SequenceSelectionViewModel();

            NavigationContentView = _sequenceSelectionView;
            CurrentView = _aboutView;           
        }

        #region Commands 
        [RelayCommand]
        private void ShowAboutControl()
        {            
            CurrentView = _aboutView;
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
