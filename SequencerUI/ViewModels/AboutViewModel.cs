using CommunityToolkit.Mvvm.ComponentModel;
using SequencerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.ViewModels
{
    public partial class AboutViewModel : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        private AboutModel _aboutData;

        #endregion

        public AboutViewModel()
        {
            AboutData = new AboutModel();            
        }
    }
}
