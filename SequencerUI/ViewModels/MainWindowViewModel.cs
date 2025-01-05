using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using SequencerUI.Helpers;
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
    public partial class MainWindowViewModel : ObservableRecipient
    {
        #region Properties
        [ObservableProperty]
        private string? _title = "Sequencer";

        [ObservableProperty]
        private object? _currentView;

        [ObservableProperty]
        private object _navigationContentView;

        [ObservableProperty]
        private bool _isMenuVisible = false;

        [ObservableProperty]
        private bool _isLeftContentVisible = true;

        private SequenceRunView _seqRunView;
        private SequenceEditView _sequenceEditView;
        private AboutView _aboutView;
        private SequenceSelectionView _sequenceSelectionView;

        private readonly IMessenger _messenger;

        #endregion


        public MainWindowViewModel(IMessenger messenger)
        {
            //Register message and handlers
            _messenger = messenger;
            _messenger.Register<GenericMessage<ViewMessage>>(this, (r, m) =>
            {
                HandleReceiveMessage(m.Content);
            });

            LinSequencer.InitializeLinSequencer();           

            _seqRunView = new SequenceRunView();
            _sequenceEditView = new SequenceEditView();
            _aboutView = new AboutView();
            _sequenceSelectionView = new SequenceSelectionView();

            _aboutView.DataContext = ServiceLocator.GetService<AboutViewModel>();
            _sequenceSelectionView.DataContext = ServiceLocator.GetService<SequenceSelectionViewModel>();

            NavigationContentView = _sequenceSelectionView;           
        }        

        #region Commands 
        [RelayCommand]
        private void MenuToggle()
        {
            IsMenuVisible = !IsMenuVisible;
            IsLeftContentVisible = !IsLeftContentVisible;
        }

        [RelayCommand]
        private void ShowAboutControl()
        {            
            CurrentView = _aboutView;            
        }

        #endregion

        #region Message handlers
        private void HandleReceiveMessage(ViewMessage content)
        {
            switch (content.Mode)
            {
                case EViewMode.None:
                    CurrentView = null;
                    break;
                case EViewMode.RunMode:
                    _seqRunView.DataContext = ServiceLocator.GetService<SequenceRunViewModel>(content.View);
                    CurrentView = _seqRunView;
                    break;
                case EViewMode.EditMode:
                    _sequenceEditView.DataContext = ServiceLocator.GetService<SequenceEditViewModel>(content.View);
                    CurrentView = _sequenceEditView;
                    break;
                case EViewMode.DeleteMode:
                    CurrentView = null;
                    break;
                default:
                    CurrentView = null;
                    break;
            }

            
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
