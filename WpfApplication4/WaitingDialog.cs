using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApplication4
{
    public class WaitingDialog : IDisposable
    {
        private const string DefaultMessage = "Please do not perform other operations.";
        private const string WatingProgramExeName = "WinWaitingExe.exe";
        private Process processWaitingWindow = null;
        private readonly string title;
        private readonly string message;
        private readonly int timeout;

        public WaitingDialog(string title, string message, int timeout = 0)
        {
            this.title = title;
            this.message = message;
            this.timeout = timeout;
            Start();
        }
        public WaitingDialog(string title, int timeout = 0)
        {
            this.title = title;
            this.message = DefaultMessage;
            this.timeout = timeout;
            Start();
        }

        public void Start()
        {
            try
            {
                string programPath = WatingProgramExeName;
                string args = $"\"{title}\" \"{message}\" {Process.GetCurrentProcess().Id} {timeout}";
                processWaitingWindow = Process.Start(new ProcessStartInfo(programPath, args));
            }
            catch (Exception ex) 
            {
                //log
            }
        }
        public void Close()
        {
            try
            {
                processWaitingWindow?.Kill();
                processWaitingWindow = null;
            }
            catch (Exception ex)
            {
                //log
            }
        }
        public void Dispose()
        {
            Close();
        }
    }
}
