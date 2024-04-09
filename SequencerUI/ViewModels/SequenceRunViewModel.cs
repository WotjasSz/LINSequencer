using CommunityToolkit.Mvvm.ComponentModel;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class SequenceRunViewModel : ObservableObject
    {
        [ObservableProperty]
        private SequenceModel _sequence;

        public SequenceRunViewModel()
        {
            //TODO Dodać nowy konstruktor bez parametrowy
            //_sequence = new SequenceModel();
        }

        public SequenceRunViewModel(SequenceModel sequence)
        {
            Sequence = sequence;            
        }
    }
}
