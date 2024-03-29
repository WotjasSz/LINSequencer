using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
        [ObservableProperty]
        private string? _title = "Sequencer";

        [ObservableProperty]
        private ObservableCollection<SequenceModel>? _availableSequences;
        
        [ObservableProperty]
        private SequenceModel? _availableSeqSelected;

        [ObservableProperty]
        private ObservableCollection<SequenceModel>? _activeSequences;

        //public ICommand AddSequenceCommand { get; }


        public MainWindowViewModel()
        {
            //AddSequenceCommand = new RelayCommand(AddSequence);

            LinSequencer.InitializeLinSequencer();

            _availableSequences = new ObservableCollection<SequenceModel>(LinSequencer.SequenceList);
            _activeSequences = new ObservableCollection<SequenceModel>();
        }

        [RelayCommand]
        private void AddSequence()
        {
            if(AvailableSeqSelected != null)
            {
                ActiveSequences.Add(AvailableSeqSelected);
            }            
            //Debug.WriteLine("Dobra jestem tutaj..........");
        }

        private bool CanExeuteAddSequence()
        {
            return AvailableSeqSelected != null;
        }

    }
}
