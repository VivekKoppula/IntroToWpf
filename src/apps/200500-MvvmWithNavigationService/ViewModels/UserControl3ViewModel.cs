using MvvmWithNavigationService.Infra;
using MvvmWithNavigationService.Services;

namespace MvvmWithNavigationService.ViewModels
{
    public class UserControl3ViewModel : BaseViewModel
    {
        public string Title { get; set; } = "View 3";

        public UserControl3ViewModel(INavigationService navigationService)
        {
            // Do we need both here.
            _navigationService = navigationService;
            Navigation = navigationService;

            NavigateToOne = new RelayCommand(execute: o => { Navigation.NavigateTo<UserControl1ViewModel>(); }, canExecute: o => true);
            NavigateToTwo = new RelayCommand(execute: o => { Navigation.NavigateTo<UserControl2ViewModel>(); }, canExecute: o => true);

        }

        private INavigationService _navigationService;

        public INavigationService Navigation
        {
            get { return _navigationService; }
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToOne { get; set; }
        public RelayCommand NavigateToTwo { get; set; }
    }
}
