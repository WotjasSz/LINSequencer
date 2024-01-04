using Caliburn.Micro;
using SequenceBuilderUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceBuilderUI.ViewModels
{
    public class AboutViewModel : Screen
    {
        private AboutModel _aboutData = new AboutModel();

        public AboutModel AboutData
        {
            get { return _aboutData; }
        }

        public Task CloseForm()
        {
            return TryCloseAsync(true);
        }
    }
}
