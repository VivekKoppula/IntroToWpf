using System;

namespace ListBoxScrollViewer.Infra
{
    public interface IPageViewModel
    {
        event EventHandler<EventArgs<string>>? ViewChanged;
        string PageId { get; set; }
        string Title { get; set; }
    }
}
