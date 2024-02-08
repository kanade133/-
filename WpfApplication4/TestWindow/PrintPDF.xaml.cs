using Microsoft.Win32;
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

namespace WpfApplication4.TestWindow
{
    /// <summary>
    /// PrintPDF.xaml 的交互逻辑
    /// </summary>
    public partial class PrintPDF : Window
    {
        private const double CanvasWidth = 1123;
        private const double CanvasHeight = 784;
        public PrintPDF()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                var size = new Size(CanvasWidth, CanvasHeight);
                var transform = this.canvas.RenderTransform;
                this.canvas.RenderTransform = Transform.Identity;

                this.gridRoot.Measure(size);
                this.gridRoot.Arrange(new Rect(new Point(0, 0), size));

                printDialog.PrintVisual(this.canvas, "print");
                this.canvas.RenderTransform = transform;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // 创建一个RenderTargetBitmap对象，它用于捕获窗口的内容
            var bitmap = new RenderTargetBitmap((int)ActualWidth, (int)ActualHeight, 96, 96, PixelFormats.Default);
            // 将窗口的内容绘制到RenderTargetBitmap上
            bitmap.Render(this.gridRoot);
            // 创建一个Encoder对象，它用于将位图保存为指定格式的图片
            var encoder = new PngBitmapEncoder();
            // 将RenderTargetBitmap添加到编码器中
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            // 创建一个SaveFileDialog对象，让用户选择保存位置和文件名
            var dialog = new SaveFileDialog { Filter = "PNG 图片|*.png" };
            if (dialog.ShowDialog() == true)
            {
                // 打开文件流并将编码器写入文件
                using (var stream = new System.IO.FileStream(dialog.FileName, System.IO.FileMode.Create))
                {
                    encoder.Save(stream);
                }
            }
        }
    }
}
