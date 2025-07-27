using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using LINSequencerLib.Settings;
using LINSequencerLib.SupportingFiles;
using SequencerUI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class SequenceSelectionViewModel : ObservableRecipient
    {
        #region Fields
        [ObservableProperty]
        private bool _isAddSeqPanelEnable;

        [ObservableProperty]
        private bool _isPanelButtonEnabled = true;

        [ObservableProperty]
        private ObservableCollection<ISequenceModel>? _availableSequences;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddSequenceCommand))]
        private SequenceModel? _availableSeqSelected;

        [ObservableProperty]
        private ObservableCollection<ISequenceModel>? _activeSequences;

        [ObservableProperty]
        private ISequenceModel? _currentSequence;

        private bool _editModeAvailable = AppConfig.AdminMode;

        private readonly IMessenger _messenger;

        #endregion

        public SequenceSelectionViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            AvailableSequences = new ObservableCollection<ISequenceModel>(LinSequencer.SequenceList);
            ActiveSequences = new ObservableCollection<ISequenceModel>(LinSequencer.ActiveSequenceList);
        }

        #region Property actions
        partial void OnCurrentSequenceChanged(ISequenceModel? value)
        {
            //TODO zmienić to na tworzenie jednego obiektu i update sequence a nie za każdym razem tworzenie od nowa
            // Być moze użycie AutoFac??            
            if (value != null)
            {
                ViewMessage message = new ViewMessage(EViewMode.RunMode, value);
                _messenger.Send(new GenericMessage<ViewMessage>(message));
            }
        }
        #endregion

        #region Commands

        [RelayCommand(CanExecute = nameof(CanExeuteAddSequence))]
        private void AddSequence()
        {
            if (AvailableSeqSelected != null)
            {
                ActiveSequences.Add(AvailableSeqSelected.DeepCloneJson());
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
            AvailableSequences = new ObservableCollection<ISequenceModel>(LinSequencer.SequenceList);
        }

        [RelayCommand(CanExecute = nameof(CanExecuteAddNewSequence))]
        private void AddNewSequence()
        {            
            SequenceModel sequenceModel = new SequenceModel();
            ActiveSequences.Add(sequenceModel);
            CurrentSequence = sequenceModel;
            
            //Send message to MainView
            ViewMessage message = new ViewMessage(EViewMode.EditMode, sequenceModel);
            _messenger.Send(new GenericMessage<ViewMessage>(message));
            
            //Hide Add panel
            IsAddSeqPanelEnable = !IsAddSeqPanelEnable;
            IsPanelButtonEnabled = !IsAddSeqPanelEnable;
        }

        [RelayCommand]
        private void RemoveSequence(SequenceModel seq)
        {
            if (CurrentSequence == seq)
            {                
                ViewMessage message = new ViewMessage(EViewMode.DeleteMode, seq);
                _messenger.Send(new GenericMessage<ViewMessage>(message));
            }
            seq.DisconnectDevice();
            ActiveSequences.Remove(seq);
        }

        [RelayCommand(CanExecute = nameof(CanExecuteEditSequence))]
        private void EditSequence(SequenceModel seq)
        {
            if (seq.State == ESequenceState.Running) return;
            seq.DisconnectDevice();
            ViewMessage message = new ViewMessage(EViewMode.EditMode, seq);
            _messenger.Send(new GenericMessage<ViewMessage>(message));
        }

        [RelayCommand]
        private void DuplicateSequence(SequenceModel seq)
        {
            ActiveSequences.Add(seq.DeepCloneJson());
        }

        private bool CanExeuteAddSequence()
        {
            return AvailableSeqSelected != null;
        }

        private bool CanExecuteEditSequence()
        {
            return _editModeAvailable;
        }

        private bool CanExecuteAddNewSequence()
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
    }
}
