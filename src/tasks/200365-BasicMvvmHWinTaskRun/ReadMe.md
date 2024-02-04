# Basic Wpf App with Mvvm.

1. Reference:
https://www.technical-recipes.com/2022/navigating-between-views-in-wpf-mvvm-using-dependency-injection/

2. First create a regular Wpf project. Add Views and ViewModel Folders.

3. In the App.xaml, remove the startup uri attribute(StartupUri="MainWindow.xaml")

4. In each of the subsequent views(xaml files) that you add, add the following attributes as appropriate.
```xml
xmlns:localVs="clr-namespace:BasicMvvmHWinTaskRun.Views"             
xmlns:localVms="clr-namespace:BasicMvvmHWinTaskRun.ViewModels"
```
5. You need to wait for a second to populate the window handle. Right at the start of the wpf app, the process is still starting. The Process Id is ready, but the Main Window Handle of the wpf app is still not ready. The following will return 0 in ctor. We need to use Task.Run, where we wait for a second.

```cs
public MainWindowViewModel()
{
    ProcessId = Process.GetCurrentProcess().Id.ToString();
    // At this point, right at the start of the wpf app, the process is still starting.
    // The Process Id is ready, but the Main Window Handle of the wpf app is still not ready.
    // The following will return 0.
    // So the following assignment is useless. 
    HWnd = Process.GetCurrentProcess().MainWindowHandle.ToString();
            
    // We need to wait some time till the window handle is ready.
    // So the following is needed.
    Task.Run(async () => {
        await GetAndAssignWindowHandle();
    });
}

private async Task GetAndAssignWindowHandle()
{
    await Task.Delay(1000);
    HWnd = Process.GetCurrentProcess().MainWindowHandle.ToString();
}
```

