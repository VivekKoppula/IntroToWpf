# Introduces Delendency injection

1. Start from the basic wpf application.

2. Add a new ClassLib project to the solution. 

3. Add a simple class along with an interface, with one string type prop called Data. 

4. In the App.xaml file, remove the StartupUri="MainWindow.xaml

5. Add the following nuget packages to the UI project.

```xml
<ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="7.0.1" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="7.0.0" />
</ItemGroup>
```

6. Configure the AppHost in the App ctro, then also add the OnStartup and OnExit. See the App.xaml.cs file.

7. Configure the services as follows.

```cs
services.AddSingleton<MainWindow>();
services.AddTransient<IDataAccess, DataAccess>();
```
8. Configure the MainWindow class ctor to recieve a IDataAccess object. The IDataAccess is a dependency for MainWindow class. Then assign the IDataAccess as DataContext to the MainWindow class.

9. In the main window replace the Grid with Stackpannel and then add a text block 

```cs
<TextBlock x:Name="Data" Padding="20" Text="{Binding Data}"  />
```

10. Now run the app.

## Ref
1. https://www.youtube.com/watch?v=dLR_D2IJE1M






