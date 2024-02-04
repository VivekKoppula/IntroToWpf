# Introduces Factory pattern in wpf in conjunction with Dependency injection

1. Start from earlier 200240-WpfDi project.
2. To the UI Project, add a window called ChildForm.
3. Add this child form as a service.
```cs
services.AddTransient<ChildForm>();
```
4. Let the MainWindow have this childForm as a dependency.

```cs
private readonly ChildForm _childForm;
public MainWindow(IDataAccess dataAccess, ChildForm childForm)
{

    DataContext = dataAccess;
    InitializeComponent();
    _childForm = childForm;
}
```

5. Also add a button to the main window which will show the child form when clicked.

```cs
private void OpenChildForm_Click(object sender, RoutedEventArgs e)
{
    _childForm..Show();
}
```

6. The problem with this is, we cant use this method to show multiple child forms. One child form opens, but not many. 

7. So introducing factory pattern.

8. Add a Utilities folder and Add a class AbstractFactory, then extract an interface IAbstractFactory.

9. Add a ServiceExtensions class which will hold an extension method to register services as follows.
```cs

serviceCollection.AddTransient<TForm>();
// This Func<TForm> is a delegate. And this delegate will create that form when ever we need. 
// So here, the delegate is added as a service, not the form. 
serviceCollection.AddSingleton<Func<TForm>>(x => () => x.GetService<TForm>()!);
serviceCollection.AddSingleton<IAbstactFactory<TForm>, AbstactFactory<TForm>>();

```
10. Now modify the MainWindow, the ctor, the click evnnt of the button as well.
11. Now if you click multiple times, the button, it will open multiple forms.
12. Also note, the child from itself can have dependencies. See the ctor of the child form.

13. Question to ponder. Do you think the following registration is necessary? Commentout that and see. You will get an exception.
```cs
serviceCollection.AddTransient<TForm>();
```
14. If you want to explore, replace the following 

```cs
serviceCollection.AddSingleton<Func<TForm>>(x => () => x.GetService<TForm>()!);
```

with the following. Place break points and then debug.

```cs
serviceCollection.AddSingleton<Func<TForm>>(x =>
	() => 
	{
		var tform = x.GetService<TForm>()!;
		return tform;
	});

```

## Reference.
https://www.youtube.com/watch?v=dLR_D2IJE1M&t=1486s

