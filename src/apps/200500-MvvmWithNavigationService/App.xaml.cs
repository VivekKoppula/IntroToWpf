
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System;
using MvvmWithNavigationService.ViewModels;
using MvvmWithNavigationService.Services;
using MvvmWithNavigationService.Infra;
using MvvmWithNavigationService.Views;

namespace MvvmWithNavigationService
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            services.AddSingleton(serviceProvider => new MainWindow
            {
                DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>()
            });

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<UserControl1ViewModel>();
            services.AddSingleton<UserControl2ViewModel>();
            services.AddSingleton<UserControl3ViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Working
            // services.AddSingleton<Func<Type, BaseViewModel>>(serviceProvider => viewModelType => (BaseViewModel)serviceProvider.GetRequiredService(viewModelType));

            // Working
            //services.AddSingleton<Func<Type, BaseViewModel>>((serviceProvider) =>
            //{
            //    return viewModelType => (BaseViewModel)serviceProvider.GetRequiredService(viewModelType);
            //});

            // Working
            services.AddSingleton<Func<Type, BaseViewModel>>((serviceProvider) =>
            {
                return viewModelType =>
                {
                    var viewModel = (BaseViewModel)serviceProvider.GetRequiredService(viewModelType);
                    return viewModel;
                };
            });

            _serviceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var startupForm = _serviceProvider.GetRequiredService<MainWindow>();
            startupForm!.Show();
            base.OnStartup(e);
        }
    }
}
