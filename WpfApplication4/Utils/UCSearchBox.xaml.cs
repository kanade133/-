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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication4
{
    /// <summary>
    /// UCSearchBox.xaml 的交互逻辑
    /// </summary>
    public partial class UCSearchBox : UserControl
    {
        public UCSearchBox()
        {
            InitializeComponent();
        }

        public string HintText
        {
            get { return (string)GetValue(HintTextProperty); }
            set { SetValue(HintTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HintText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintTextProperty =
            DependencyProperty.RegisterAttached("HintText", typeof(string), typeof(UCSearchBox), new PropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(UCSearchBox), new PropertyMetadata(null));

        public static readonly RoutedEvent OnTextCommittedEvent = EventManager.RegisterRoutedEvent("OnTextCommittedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UCSearchBox));
        public event RoutedEventHandler OnTextCommitted
        {
            add { AddHandler(OnTextCommittedEvent, value); }
            remove { RemoveHandler(OnTextCommittedEvent, value); }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.RaiseEvent(new RoutedEventArgs(OnTextCommittedEvent));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(OnTextCommittedEvent));
        }
    }
}
