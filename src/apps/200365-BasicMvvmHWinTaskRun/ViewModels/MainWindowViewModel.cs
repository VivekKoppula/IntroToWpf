using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BasicMvvmHWinTaskRun.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        public string Number { get; set; } = "One";
        public string ProcessId { get; set; } 
        public string _hWnd = string.Empty;

        public string HWnd
        {
            get
            {
                return _hWnd;
            }
            set
            {
                _hWnd = value;
                OnPropertyChanged(nameof(HWnd));
            }
        }

        public MainWindowViewModel()
        {
            ProcessId = Process.GetCurrentProcess().Id.ToString();
            // At this point, right at the start of the wpf app, the process is still starting.
            // The Process Id is ready, but the Main Window Handle of the wpf app is still not ready.
            // The following will return 0.
            // So the following assignment is useless. 
            HWnd = Process.GetCurrentProcess().MainWindowHandle.ToString();
            
            // We need to wait some time till the window handle is ready.
            // So the following is needed.
            Task.Run(async () => {
                await GetAndAssignWindowHandle();
            });
        }

        private async Task GetAndAssignWindowHandle()
        {
            await Task.Delay(1000);
            HWnd = Process.GetCurrentProcess().MainWindowHandle.ToString();
        }

    }
}
