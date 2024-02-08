using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WinWaitingExe
{
    /// <summary>
    /// WinWaiting.xaml 的交互逻辑
    /// </summary>
    public partial class WinWaiting : Window
    {
        public WinWaiting(string title, string message)
        {
            InitializeComponent();

            this.ShowInTaskbar = false;
            this.Topmost = true;
            this.Title = title;
            this.textBlock.Text = message;
        }
    }
}
