﻿using System;
using System.Windows.Input;
using BasicMvvm.Infra;

namespace BasicMvvm.ViewModels
{
    public class UserControl1ViewModel : IPageViewModel
    {
        private ICommand? _goTo2;
        private ICommand? _goTo3;

        public event EventHandler<EventArgs<string>>? ViewChanged;
        public string PageId { get; set; }
        public string Title { get; set; }

        public UserControl1ViewModel(string pageIndex = "1")
        {
            PageId = pageIndex;
            Title = "View 1";
        }
        public ICommand GoTo2
        {
            get
            {
                return _goTo2 ??= new RelayCommand(x =>
                {
                    ViewChanged?.Raise(this, "2");
                });
            }
        }

        public ICommand GoTo3
        {
            get
            {
                return _goTo3 ??= new RelayCommand(x =>
                {
                    ViewChanged?.Raise(this, "3");
                });
            }
        }
    }

}
