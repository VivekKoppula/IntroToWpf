using BasicMvvmVarOne.ViewModels;
using BasicMvvmVarOne.Infra;
using System.Collections.Generic;
using System.Windows.Input;
using System;

namespace BasicMvvmVarOne.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IPageViewModel? _pageViewModel;

        private ICommand? _goTo1;
        private ICommand? _goTo2;
        private ICommand? _goTo3;

        public event EventHandler<EventArgs<string>>? ViewChanged;

        private readonly Dictionary<string, IPageViewModel>? _pageViewModels = new();

        public IPageViewModel? CurrentPageViewModel
        {
            get
            {
                return _pageViewModel;
            }
            set
            {
                _pageViewModel = value;
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
        }


        public MainWindowViewModel(IDataModel pageViews)
        {
            _pageViewModels["1"] = new UserControl1ViewModel("1");
            //_pageViewModels["1"].ViewChanged += (o, s) =>
            //{
            //    CurrentPageViewModel = _pageViewModels[s.Value];
            //    pageViews.Data = "Data: " + s.Value.ToString();
            //};

            _pageViewModels["2"] = new UserControl2ViewModel("2");
            //_pageViewModels["2"].ViewChanged += (o, s) =>
            //{
            //    CurrentPageViewModel = _pageViewModels[s.Value];
            //    pageViews.Data = "Data: " + s.Value.ToString();
            //};

            _pageViewModels["3"] = new UserControl3ViewModel("3");
            //_pageViewModels["3"].ViewChanged += (o, s) =>
            //{
            //    CurrentPageViewModel = _pageViewModels[s.Value];
            //    pageViews.Data = "Data: " + s.Value.ToString();
            //};

            CurrentPageViewModel = _pageViewModels["1"];
        }

        public ICommand GoTo1
        {
            get
            {
                return _goTo1 ??= new RelayCommand(x =>
                {
                    CurrentPageViewModel = _pageViewModels?["1"];
                });
            }
        }

        public ICommand GoTo2
        {
            get
            {
                return _goTo2 ??= new RelayCommand(x =>
                {
                    CurrentPageViewModel = _pageViewModels?["2"];
                });
            }
        }

        public ICommand GoTo3
        {
            get
            {
                return _goTo3 ??= new RelayCommand(x =>
                {
                    CurrentPageViewModel = _pageViewModels?["3"];
                });
            }
        }
    }
}
