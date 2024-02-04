using System.Windows;
using DataLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfUi.Utilities;

namespace WpfUi;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<MainWindow>();
            // services.AddTransient<ChildForm>();
            services.AddFormFactory<ChildForm>();
            services.AddTransient<IDataAccess, DataAccess>();
            
        }).Build();
    }
    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();
        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        //startupForm!.DataContext = new MainWindowViewModel(new DataModel { Data = "Placeholder" });
        startupForm!.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }


    public static IHost? AppHost { get; private set; }
}
