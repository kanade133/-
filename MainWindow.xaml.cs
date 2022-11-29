using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Item> _listItems;

        public MainWindow()
        {
            InitializeComponent();

            _listItems = new ObservableCollection<Item>();
            int cout = _listItems.Count;
            var r = new Random();
            for (int i = _listItems.Count; i < cout + 500; i++)
            {
                var item = new Item();
                item.Id = i;
                item.Items = new List<Item>();
                int count2 = 100;
                for (int j = 0; j < count2; j++)
                {
                    var item2 = new Item() { Id = r.Next() };
                    item.Items.Add(item2);
                }
                _listItems.Add(item);
            }

            //this.listBox.ItemsSource = _listItems;
            this.dg.ItemsSource = _listItems;
            this.dg2.ItemsSource = _listItems;
            this.dg3.ItemsSource = _listItems;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int cout = _listItems.Count;
            var r = new Random();
            for (int i = _listItems.Count; i < cout + 500; i++)
            {
                var item = new Item();
                item.Id = i;
                item.Items = new List<Item>();
                int count2 = 100;
                for (int j = 0; j < count2; j++)
                {
                    var item2 = new Item() { Id = r.Next() };
                    item.Items.Add(item2);
                }
                _listItems.Add(item);
            }
        }

        private class Item
        {
            public int Id { get; set; }
            public List<Item> Items { get; set; }
        }
    }
}
