﻿using System;
using System.Windows.Input;
using BasicMvvm.Infra;

namespace BasicMvvm.ViewModels
{
    public class UserControl3ViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand? _goTo1;
        private ICommand? _goTo2;

        public event EventHandler<EventArgs<string>>? ViewChanged;
        public string PageId { get; set; }
        public string Title { get; set; } = "View 3";

        public UserControl3ViewModel(string pageIndex = "3")
        {
            PageId = pageIndex;
        }

        public ICommand GoTo1
        {
            get
            {
                return _goTo1 ??= new RelayCommand(x =>
                {
                    ViewChanged?.Raise(this, "1");
                });
            }
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
    }
}
