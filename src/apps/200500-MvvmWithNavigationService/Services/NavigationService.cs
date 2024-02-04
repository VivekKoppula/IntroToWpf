using MvvmWithNavigationService.Infra;
using System;
using System.Diagnostics;

namespace MvvmWithNavigationService.Services
{
    public interface INavigationService
    {
        BaseViewModel CurrentView { get; }

        void NavigateTo<T>() where T : BaseViewModel;
    }
    public class NavigationService : ObservableObject, INavigationService
    {
        private BaseViewModel? _currentView;
        public BaseViewModel CurrentView
        {
            get => _currentView!;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private readonly Func<Type, BaseViewModel> _viewModelFactory;

        public NavigationService(Func<Type, BaseViewModel> viewModelFactory)
        {
            Debugger.Break();
            _viewModelFactory = viewModelFactory;

            // Don't try the following. This would not work. The reason is, the following is
            // trying to get an instance of UserControl1ViewModel. But UserControl1ViewModel itself has a dependency on NavigationService
            // So is a cyclic dependency. This would not work.

            // CurrentView = _viewModelFactory.Invoke(typeof(UserControl1ViewModel));

        }

        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            BaseViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));

            // Never assign to the field, always assign to prop. Why? You ask?
            // Assignemnt to prop, calls OnPropertyChanged() as well.
            // _currentView = viewModel;
            CurrentView = viewModel;
        }
    }
}
