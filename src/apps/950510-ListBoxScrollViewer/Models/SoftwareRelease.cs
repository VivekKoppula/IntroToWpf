using ListBoxScrollViewer.Infra;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListBoxScrollViewer.Models
{
    public class SoftwareRelease : BaseModel
    {
        public string FullVersionNumber { get; }

        public string DisplayVersionNumber { get; }

        public string DisplayDate { get; set; }

        public ObservableCollection<SoftwareReleaseNotes> Notes { get; }

        private bool _isOpen = false;
        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                _isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
            }
        }

        private bool _isUnread = true;
        public bool IsUnread
        {
            get => _isUnread;
            set
            {
                _isUnread = value;
                OnPropertyChanged(nameof(IsUnread));
            }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        private bool _isLast = false;
        public bool IsLast
        {
            get => _isLast;
            set
            {
                _isLast = value;
                OnPropertyChanged(nameof(IsLast));
            }
        }

        public SoftwareRelease(string fullVersionNumber, string displayVersionNumber, string displayDate)
        {
            Notes = new ObservableCollection<SoftwareReleaseNotes>();
            FullVersionNumber = fullVersionNumber;
            DisplayVersionNumber = displayVersionNumber;
            DisplayDate = displayDate;
        }
    }
}
