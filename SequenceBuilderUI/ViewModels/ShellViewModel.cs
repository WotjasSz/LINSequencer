using Caliburn.Micro;
using LINSequencerLib;
using System.Threading;
using System.Threading.Tasks;

namespace SequenceBuilderUI.ViewModels
{
    public class ShellViewModel : Conductor<Screen>.Collection.AllActive
    {
        #region Fields
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private SidePanelViewModel _sidePanel;
        private MainPanelViewModel _mainPanel;
        #endregion

        #region Properties
        public SidePanelViewModel SidePanel
        {
            get { return _sidePanel; }
        }

        public MainPanelViewModel MainPanel
        {
            get { return _mainPanel; }
        }

        #endregion
        public ShellViewModel(IWindowManager windowManager, IEventAggregator eventAggregator, SidePanelViewModel seqNaviVM, MainPanelViewModel mainPanel)
        {
            LinSequencer.InitializeLinSequencer();
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _sidePanel = seqNaviVM;
            _mainPanel = mainPanel;

            SidePanel.ConductWith(this);
            MainPanel.ConductWith(this);
        }

        public Task CloseWidnow()
        {
            return TryCloseAsync();
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return base.OnActivateAsync(cancellationToken);
        }

        public Task About()
        {
            return _windowManager.ShowDialogAsync(IoC.Get<AboutViewModel>());
        }
    }
}
