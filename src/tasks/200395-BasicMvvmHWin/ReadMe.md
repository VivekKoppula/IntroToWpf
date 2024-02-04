# Basic Wpf App with Mvvm.

1. Reference:
https://www.technical-recipes.com/2022/navigating-between-views-in-wpf-mvvm-using-dependency-injection/

2. First create a regular Wpf project. Add Views and ViewModel Folders.

3. In the App.xaml, remove the startup uri attribute(StartupUri="MainWindow.xaml")

4. In each of the subsequent views(xaml files) that you add, add the following attributes as appropriate.
```xml
xmlns:localVs="clr-namespace:BasicMvvmHWin.Views"             
xmlns:localVms="clr-namespace:BasicMvvmHWin.ViewModels"
```

5. We need to Click the button.

```cs
private ICommand? _hWinButtonClick;

public ICommand HWinButtonClick
{
    get
    {
        return _hWinButtonClick ??= new RelayCommand(x =>
        {
            HWnd = Process.GetCurrentProcess().MainWindowHandle.ToString();
        });
    }
}

```

