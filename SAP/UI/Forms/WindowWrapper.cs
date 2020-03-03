using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SapFramework.SAP.UI.Forms
{
    [DebuggerStepThrough]
    internal class WindowWrapper : IWin32Window
    {
        public WindowWrapper()
        {
            this.Handle = GetForegroundWindow();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public IntPtr Handle { get; private set; }
    }
}
