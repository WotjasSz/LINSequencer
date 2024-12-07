using Microsoft.Extensions.DependencyInjection;
using SequencerUI.ViewModels;
using SequencerUI.Views;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows;


namespace SequencerUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindowView = ServiceProvider.GetRequiredService<MainWindowView>();
            mainWindowView.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Registering View and ViewModels
            var assembly = Assembly.GetExecutingAssembly();

            RegisterTypes(services, assembly, "SequencerUI.Views");
            RegisterTypes(services, assembly, "SequencerUI.ViewModels");
            
            // Registartation of the Services

        }

        private void RegisterTypes(IServiceCollection services, Assembly assembly, string namespacePrefix) 
        { 
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace == namespacePrefix)
                .ToList(); 
            
            foreach (var type in types) 
            { 
                services.AddTransient(type); 
            } 
        }
    }
}
