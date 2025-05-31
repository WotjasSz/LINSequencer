using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib.Sequence;
using Microsoft.Extensions.DependencyInjection;
using SequencerUI.ViewModels;
using SequencerUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.Services
{
    public static class ServiceLocator
    {
        private static IServiceProvider _serviceProvider;

        public static void ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            //Services registration
            serviceCollection.AddSingleton<IMessenger, WeakReferenceMessenger>();
            serviceCollection.AddSingleton<IFileDialogService, FileDialogService>();

            //Main View registration 
            serviceCollection.AddTransient<MainWindowView>();

            // Registering All Views and ViewModels
            var assembly = Assembly.GetExecutingAssembly();
            //var externalAssembly = Assembly.Load("LINSequencerLib");

            //RegisterExternalTypes(serviceCollection, externalAssembly, "LINSequencerLib.Sequence");
            //RegisterViews(serviceCollection, assembly);
            RegisterViewModels(serviceCollection, assembly);

            // Registartation of the Services
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void RegisterExternalTypes(ServiceCollection serviceCollection, Assembly assembly, string nsPrefix)
        {
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace != null && t.Namespace == nsPrefix)
                .ToList();
            foreach (var type in types)
            {
                foreach (var iface in type.GetInterfaces())
                {
                    serviceCollection.AddTransient(iface, type);
                }
            }
        }

        /// <summary>
        /// Register all views in the Assembly considering that all Views has in constructor only one parameter which is coresponding ViewModel
        /// </summary>
        /// <param name="services">Reference to Service collection</param>
        /// <param name="assembly">Reference to Assembly</param>
        private static void RegisterViews(IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace == "SequencerUI.Views")
                .ToList();

            foreach (var type in types)
            {
                services.AddTransient(type);
            }
        }

        /// <summary>
        /// Register all ViewModels
        /// </summary>
        /// <param name="services">Reference to Service collection</param>
        /// <param name="assembly">Reference to Assembly</param>
        private static void RegisterViewModels(IServiceCollection services, Assembly assembly)
        {
            var viewModelTypes = assembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace == "SequencerUI.ViewModels" && !t.IsNestedPrivate)
                .ToList();
            foreach (var viewModelType in viewModelTypes)
            {
                services.AddTransient(viewModelType);
                //RegisterViewModelWithFactory(services, viewModelType);
            }
        }

        /// <summary>
        /// Register givent type in the services list, collecting all needed parameters for contructor.
        /// </summary>
        /// <param name="services">Reference to Service collection</param>
        /// <param name="viewModelType">Type of ViewModel</param>
        private static void RegisterViewModelWithFactory(IServiceCollection services, Type viewModelType)
        {
            var constructors = viewModelType.GetConstructors();

            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                if (parameters.Length == 0)
                {
                    // Brak zależności, zwykła rejestracja
                    services.AddTransient(viewModelType);
                }
                else
                {
                    // Utworzenie fabryki z zależnościami
                    services.AddTransient(serviceProvider =>
                    {
                        return (Func<object[], object>)(args =>
                        {
                            var parameterInstances = parameters.Select((p, index) =>
                            {
                                if (index < args.Length && args[index] != null && args[index].GetType() == p.ParameterType)
                                {
                                    return args[index];
                                }
                                else
                                {
                                    return serviceProvider.GetService(p.ParameterType) ?? Activator.CreateInstance(p.ParameterType);
                                }
                            }).ToArray();
                            return Activator.CreateInstance(viewModelType, parameterInstances);
                        });
                    });
                }
            }
        }

        /// <summary>
        /// Methods return an instance of the requested type. Use only if type have constructor without parameters or parameters are registerd in DI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>() => _serviceProvider.GetRequiredService<T>();

        /// <summary>
        /// Methods return an instance of the requested type with parameters. Method check given parameter list with all type's contructors and select proper one.
        /// If constructor has parameter which are registered in the DI, automatically add it to the constructor.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">Paramaters which aren't registered in the DI container</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T GetService<T>(params object[] parameters)
        {
            var type = typeof(T);
            var constructors = type.GetConstructors();

            foreach (var constructor in constructors.OrderByDescending(c => c.GetParameters().Length))
            {
                var paramInfos = constructor.GetParameters();
                var paramInstances = new List<object>();
                bool canConstruct = true;

                foreach (var paramInfo in paramInfos)
                {
                    // Sprawdzenie, czy parametr jest już zarejestrowany w DI
                    var service = _serviceProvider.GetService(paramInfo.ParameterType);
                    if (service != null)
                    {
                        paramInstances.Add(service);
                    }
                    else
                    {
                        // Sprawdzenie, czy parametr jest na liście przekazanych parametrów
                        var parameter = parameters.FirstOrDefault(p => p.GetType() == paramInfo.ParameterType);
                        if (parameter != null)
                        {
                            paramInstances.Add(parameter);
                        }
                        else
                        {
                            canConstruct = false;
                            break;
                        }
                    }
                }

                // Utworzenie instancji z wszystkimi parametrami
                if (canConstruct)
                {
                    return (T)constructor.Invoke(paramInstances.ToArray());
                }
            }

            throw new ArgumentException($"No matching constructor found for type {type}");
        }
    }
}
