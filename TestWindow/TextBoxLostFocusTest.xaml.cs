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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class TextBoxLostFocusTest : Window
    {
        public TextBoxLostFocusTest()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 隐藏Window不会触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = "TextBox_LostFocus";
        }
        /// <summary>
        /// 隐藏Window会触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            textBox.Text = "textBox_LostKeyboardFocus";
        }
    }
}
