using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WinWaitingExe
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string title = string.Empty;
            string message = string.Empty;
            if (e.Args.Length > 0)
            {
                title = e.Args[0];
            }
            if (e.Args.Length > 1)
            {
                message = e.Args[1];
            }
            if (e.Args.Length > 2 && int.TryParse(e.Args[2], out int parentPID) && parentPID != 0)
            {
                var parentProcess = Process.GetProcessById(parentPID);
                Task.Run(() =>
                {
                    parentProcess?.WaitForExit();
                    Environment.Exit(0);
                });
            }
            if (e.Args.Length > 3 && int.TryParse(e.Args[3], out int timeout) && timeout != 0)
            {
                Task.Run(() =>
                {
                    Thread.Sleep(timeout);
                    Environment.Exit(0);
                });
            }
            new WinWaiting(title, message).ShowDialog();
        }
    }
}
