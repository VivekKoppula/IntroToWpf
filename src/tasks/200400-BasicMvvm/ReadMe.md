# Basic Wpf App with Mvvm.

1. Reference:
https://www.technical-recipes.com/2022/navigating-between-views-in-wpf-mvvm-using-dependency-injection/

2. First create a regular Wpf project. Add Views and ViewModel Folders.

3. In the App.xaml, remove the startup uri attribute(StartupUri="MainWindow.xaml")

4. In each of the subsequent views(xaml files) that you add, add the following attributes as appropriate.
```xml
xmlns:localVs="clr-namespace:BasicMvvm.Views"             
xmlns:localVms="clr-namespace:BasicMvvm.ViewModels"
```

5. I am not sure about why this (IDataModel pageViews) is needed. 
