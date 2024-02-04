using DataLibrary;
using System.Windows;
using WpfUi.Utilities;

namespace WpfUi;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IAbstactFactory<ChildForm> _abstactFactory;

    public MainWindow(IDataAccess dataAccess, IAbstactFactory<ChildForm> abstactFactory)
    {

        DataContext = dataAccess;
        InitializeComponent();
        _abstactFactory = abstactFactory;
    }

    private void OpenChildForm_Click(object sender, RoutedEventArgs e)
    {
        var form = _abstactFactory.Create();
        form.Show();
    }
}