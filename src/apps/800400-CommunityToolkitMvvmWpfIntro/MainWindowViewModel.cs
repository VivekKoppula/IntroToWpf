using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommunityToolkitMvvmWpfIntro
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        
        private string number = "One";

        public string Number
        {
            get { return number; }
            set { 
                number = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(number)));
            }
        }


        public ICommand ClickCommand { get; }

        public MainWindowViewModel()
        {
            ClickCommand = new RelayCommand(OnClick, CanClick);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// You can click only when the number is one, and not anything else.
        /// </summary>
        /// <returns></returns>
        private bool CanClick() => Number == "One";

        private void OnClick()
        {
            Number = "Two";
        }
    }
}
