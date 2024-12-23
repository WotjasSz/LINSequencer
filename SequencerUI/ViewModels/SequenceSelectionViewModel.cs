using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib;
using LINSequencerLib.Sequence;
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

        private bool _editModeAvailable = true;

        private readonly IMessenger _messenger;

        #endregion

        public SequenceSelectionViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            AvailableSequences = new ObservableCollection<ISequenceModel>(LinSequencer.SequenceList);
            ActiveSequences = new ObservableCollection<ISequenceModel>();
        }

        #region Property actions
        partial void OnCurrentSequenceChanged(ISequenceModel? value)
        {
            //TODO zmienić to na tworzenie jednego obiektu i update sequence a nie za każdym razem tworzenie od nowa
            // Być moze użycie AutoFac??            
            if (value != null)
            {
                _messenger.Send(new GenericMessage<ISequenceModel>(value));
                //_seqRunView.DataContext = new SequenceRunViewModel(value);
                //ServiceLocator.Current.GetInstance<SequenceRunView>().
                //CurrentView = _seqRunView;
                //var vmFactory = ServiceLocator.GetService<SequenceRunViewModel>();
                //var vm = (SequenceRunViewModel)vmFactory(new object[] { value });
                //var sequenceRunView = ServiceLocator.GetService<SequenceRunView>();
                //sequenceRunView.DataContext = vm;
                //CurrentView = sequenceRunView;
            }
        }
        #endregion

        #region Commands

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
            AvailableSequences = new ObservableCollection<ISequenceModel>(LinSequencer.SequenceList);
        }

        [RelayCommand]
        private void AddNewSequence()
        {
            //Debug.WriteLine("Dodawanie nowej sekwencji...");
            SequenceModel sequenceModel = new SequenceModel();
            ActiveSequences.Add(sequenceModel);
            CurrentSequence = sequenceModel;
            //_sequenceEditView.DataContext = new SequenceEditViewModel(sequenceModel);
            //CurrentView = _sequenceEditView;
            IsAddSeqPanelEnable = !IsAddSeqPanelEnable;
            IsPanelButtonEnabled = !IsAddSeqPanelEnable;
        }

        [RelayCommand]
        private void RemoveSequence(SequenceModel seq)
        {
            if (CurrentSequence == seq)
            {
                //CurrentView = null;
            }
            ActiveSequences.Remove(seq);
        }

        [RelayCommand(CanExecute = nameof(CanExecuteEditSequence))]
        private void EditSequence(SequenceModel seq)
        {
            //_sequenceEditView.DataContext = new SequenceEditViewModel(seq);
            //CurrentView = _sequenceEditView;
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
    }
}
