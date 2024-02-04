using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommunityToolkitMvvmWpfObservableObj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Data context is set directly in the xaml itself as follows. 
            /*
            <Window.DataContext>
                <local:MainWindowViewModel/>
            </Window.DataContext>
            */
            // DataContext = new MainWindowViewModel();
            InitializeComponent();
        }
    }
}