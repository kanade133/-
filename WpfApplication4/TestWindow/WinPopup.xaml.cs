using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// WinPopup.xaml 的交互逻辑
    /// </summary>
    public partial class WinPopup : Window
    {
        public WinPopup()
        {
            InitializeComponent();

            var list = new List<Model>();
            var model1 = new Model() { Id = 1, Name = "张三" };
            model1.List.Add(new Model() { Id = 11, Name = "张三-1" });
            model1.List.Add(new Model() { Id = 12, Name = "张三-2" });
            list.Add(model1);
            var model2 = new Model() { Id = 2, Name = "李四" };
            model2.List.Add(new Model() { Id = 21, Name = "李四-1" });
            list.Add(model2);
            var model3 = new Model() { Id = 3, Name = "王五" };
            model3.List.Add(new Model() { Id = 31, Name = "王五-1" });
            list.Add(model3);
            this.dg.ItemsSource = list;
        }

        private class Model
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public List<Model> List { get; set; } = new List<Model>();
        }

        private void dg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var row = UITools.GetDataGridItemCurrentRow(e.GetPosition, this.dg);
            if (row != null)
            {
                this.popup.IsOpen = false;
                this.popup.IsOpen = true;
                this.popup.DataContext = ((Model)row.DataContext).List;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.popup.IsOpen = false;
        }

        CancellationTokenSource tokenSource;
        private void popup_Opened(object sender, EventArgs e)
        {
            if (tokenSource != null)
            {
                tokenSource.Cancel();
            }
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            int count = 0;
            bool isOpen = true;
            Task.Run(() =>
            {
                bool isCancel = false; ;
                while (isOpen && !isCancel)
                {
                    System.Threading.Thread.Sleep(1000);
                    this.Dispatcher.Invoke(() =>
                    {
                        if (token.IsCancellationRequested)
                        {
                            isCancel = true;
                            return;
                        }
                        if (!this.popup.IsOpen)
                        {
                            isOpen = false;
                            return;
                        }
                        if (!this.IsActive)
                        {
                            this.popup.IsOpen = isOpen = false;
                            return;
                        }
                        if (this.popup.IsMouseOver)
                        {
                            count = 0;
                        }
                        else
                        {
                            count++;
                            if (count >= 5)
                            {
                                this.popup.IsOpen = isOpen = false;
                                return;
                            }
                        }
                    });
                }
            }, token);
        }
    }
}
