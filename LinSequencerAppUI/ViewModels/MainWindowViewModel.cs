using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LinSequencerAppUI.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        string? firstName = "Wojtek";
        
        [ObservableProperty]
        string? lastName = "Sztuk";

        public string FullName => $"{FirstName} {LastName}";
    }
}
