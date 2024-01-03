using Caliburn.Micro;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using SequenceBuilderUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SequenceBuilderUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private SequencePanelViewModel _seqNaviVM;

        public ShellViewModel(SequencePanelViewModel seqNaviVM)
        {
            _seqNaviVM = seqNaviVM;
            ActivateItemAsync(_seqNaviVM);
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            LinSequencer.InitializeLinSequencer();
            return base.OnActivateAsync(cancellationToken);
        }
    }
}
