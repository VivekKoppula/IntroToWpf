﻿using System;
using System.Windows.Input;
using ListBoxScrollViewer.Infra;

namespace ListBoxScrollViewer.ViewModels
{
    public class UserControl1ViewModel : IPageViewModel
    {
        public event EventHandler<EventArgs<string>>? ViewChanged;
        public string PageId { get; set; }
        public string Title { get; set; }

        public UserControl1ViewModel(string pageIndex = "1")
        {
            PageId = pageIndex;
            Title = "View 1";
        }
    }
}
