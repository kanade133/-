using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication4
{
    /// <summary>
    /// WindowTestControl.xaml 的交互逻辑
    /// </summary>
    public partial class WindowTestControl : Window
    {
        public WindowTestControl()
        {
            InitializeComponent();
        }

        private void UCSearchBox_OnTextCommitted(object sender, RoutedEventArgs e)
        {
            this.textBox.Text = (sender as UCSearchBox).Text;
        }
    }
}
