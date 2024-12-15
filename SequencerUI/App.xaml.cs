using SequencerUI.Services;
using SequencerUI.Views;
using System.Windows;


namespace SequencerUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {      
        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceLocator.ConfigureServices();

            base.OnStartup(e);

            var mainWindowView = ServiceLocator.GetService<MainWindowView>();
            mainWindowView.Show();
        }        
    }
}
