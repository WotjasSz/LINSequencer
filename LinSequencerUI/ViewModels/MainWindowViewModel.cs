using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinSequencerUI.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string firstName = "Wojtek";

        [ObservableProperty]
        private string lastName = "Sztuk";

        //public string FullName = $"{}";
    }
}
