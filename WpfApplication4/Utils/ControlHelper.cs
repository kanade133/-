using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApplication4
{
    public static class ControlHelper
    {
        public static void TextBox_LostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox?.DataContext != null)
            {
                var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
                if (bindingExpression != null && bindingExpression.ResolvedSourcePropertyName != null)
                {
                    textBox.DataContext.GetType().GetProperty(bindingExpression.ResolvedSourcePropertyName)?.SetValue(textBox.DataContext, textBox.Text);
                }
            }
        }
    }
}
