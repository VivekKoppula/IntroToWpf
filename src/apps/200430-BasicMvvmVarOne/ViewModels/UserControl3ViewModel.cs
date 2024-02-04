using System;
using System.Windows.Input;
using BasicMvvmVarOne.Infra;

namespace BasicMvvmVarOne.ViewModels
{
    public class UserControl3ViewModel : BaseViewModel, IPageViewModel
    {
        public event EventHandler<EventArgs<string>>? ViewChanged;
        public string PageId { get; set; }
        public string Title { get; set; } = "View 3";

        public UserControl3ViewModel(string pageIndex = "3")
        {
            PageId = pageIndex;
        }
    }
}
