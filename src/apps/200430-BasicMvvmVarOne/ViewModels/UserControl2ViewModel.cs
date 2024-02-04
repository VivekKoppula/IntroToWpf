using System;
using System.Windows.Input;
using BasicMvvmVarOne.Infra;

namespace BasicMvvmVarOne.ViewModels
{
    public class UserControl2ViewModel : BaseViewModel, IPageViewModel
    {
        public event EventHandler<EventArgs<string>>? ViewChanged;
        public string PageId { get; set; }
        public string Title { get; set; } = "View 2";

        public UserControl2ViewModel(string pageIndex = "2")
        {
            PageId = pageIndex;
        }
    }
}
