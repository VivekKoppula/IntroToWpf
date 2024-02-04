using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelIntro
{
    internal class MainWindowViewModel
    {
        public string Number { get; set; } = "One";
        public string ProcessId { get; set; } = Process.GetCurrentProcess().Id.ToString();
    }
}
