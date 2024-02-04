# Basic Wpf App with Mvvm and a Navigation service.

1. This is based on https://www.youtube.com/watch?v=wFzmBZpjuAo

2. Note this uses, only the following nuget.

```xml
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="7.0.0" />
```

But instead the following.

```xml
<PackageReference Include="Microsoft.Extensions.Hosting" Version="7.0.1" />
```

3. So instead of the following as with earlier examples,

```cs
AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
{
    services.AddSingleton<MainWindow>();
    services.AddTransient<IDataModel, DataModel>();
}).Build();
```

we now, in this example do this way. we are directly 

```cs
var services = new ServiceCollection();
services.AddSingleton(serviceProvider => new MainWindow
{
    DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>()
});

services.AddSingleton<MainWindowViewModel>();
...
_serviceProvider = services.BuildServiceProvider();
```

4. When you launch the app, you see that there is no default View assigned to the ContentControl in MainWindow as CurrentView in MainWindowViewModel. 

5. If you want a default View applied, then there are a few ways to do it.

6. One way is to add the following navigation to the ctor of MainWindowViewModel
```cs
Navigation.NavigateTo<UserControl1ViewModel>();
```

1. The second way is to assign CurrentView property of NavigationService, inside the ctor of NavigationService

```cs
CurrentView = _viewModelFactory.Invoke(typeof(UserControl1ViewModel));
```

This would not work. The reason is, the following is trying to get an instance of UserControl1ViewModel. But UserControl1ViewModel itself has a dependency on NavigationService. So is a cyclic dependency. This would not work.


6. 
