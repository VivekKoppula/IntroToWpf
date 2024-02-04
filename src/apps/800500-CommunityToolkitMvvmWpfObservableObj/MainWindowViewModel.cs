using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace CommunityToolkitMvvmWpfObservableObj
{
    // https://www.youtube.com/watch?v=uVIzK2snugk
    // https://community.devexpress.com/blogs/wpf/archive/2022/05/05/wpf-view-model-generator-prism-amp-mvvm-light-support.aspx
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ClickCommand))]
        private string _firstName = "Vivek";

        // The following two methods are due to Roslyn code generation.
        // The following method is executed before the assignment.
        partial void OnFirstNameChanging(string value)
        {
            
        }

        // The following is executed after the 
        partial void OnFirstNameChanged(string value)
        {

        }

        // The following is generated when you apply RelayCommand attribute on the method OnClick
        // See the OnClick method below. It has RelayCommand attribute on it.

        //public IRelayCommand ClickCommand { get; }

        //public MainWindowViewModel()
        //{
        //    ClickCommand = new RelayCommand(OnClick, CanClick);
        //}

        private bool CanClick() => FirstName == "Vivek";

        [RelayCommand(CanExecute = nameof(CanClick))]
        private void OnClick()
        {
            FirstName = "Koppula";
        }
    }
}
