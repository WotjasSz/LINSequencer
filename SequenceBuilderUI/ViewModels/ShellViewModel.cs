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
    public class ShellViewModel : Conductor<Screen>.Collection.AllActive
    {
        #region Fields
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private SequencePanelViewModel _sequencePanel;
        #endregion

        #region Properties
        public SequencePanelViewModel SequencePanel
        {
            get {  return _sequencePanel; }
        }

        #endregion
        public ShellViewModel(IWindowManager windowManager, IEventAggregator eventAggregator, SequencePanelViewModel seqNaviVM)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _sequencePanel = seqNaviVM;
            SequencePanel.ConductWith(this);
        }

        public Task CloseWidnow()
        {
            return TryCloseAsync();
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            LinSequencer.InitializeLinSequencer();
            return base.OnActivateAsync(cancellationToken);
        }

        public Task About()
        {
            return _windowManager.ShowDialogAsync(IoC.Get<AboutViewModel>());
        }
    }
}
