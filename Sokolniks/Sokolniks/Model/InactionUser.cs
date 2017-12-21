using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Sokolniks.Model
{
    class InactionUser
    {
        private DispatcherTimer _timer;

        public delegate void metod();
        public metod del;
        public InactionUser(metod del)
        {
            this.del = del;
        }
        private void IdleTick(object sender, EventArgs e)
        {
            var idle = GetIdle();
            if (idle.Minutes>=1 && idle.Seconds >= 30)
            {
                if (del != null)
                {
                    del();
                }
            }
        }
        public void Start()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 3);
            _timer.Tick += IdleTick;
            _timer.Start();
        }
        TimeSpan GetIdle()
        {
            var lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);

            GetLastInputInfo(ref lastInputInfo);

            var lastInput = DateTime.Now.AddMilliseconds(
                -(Environment.TickCount - lastInputInfo.dwTime));
            return DateTime.Now - lastInput;
        }

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [StructLayout(LayoutKind.Sequential)]
        internal struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }
    }
}
