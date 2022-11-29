using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WpfApplication4
{
    public class PasswordBoxBindingBehavior
    {
        /// <summary>
        /// Gets or sets the Password property on the PasswordBox control. This is a dependency property.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty
            = DependencyProperty.RegisterAttached(
                "Password",
                typeof(string),
                typeof(PasswordBoxBindingBehavior),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordPropertyChanged));

        /// <summary>Helper for getting <see cref="PasswordProperty"/> from <paramref name="dpo"/>.</summary>
        /// <param name="dpo"><see cref="DependencyObject"/> to read <see cref="PasswordProperty"/> from.</param>
        /// <returns>Password property value.</returns>
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        public static string GetPassword(DependencyObject dpo)
        {
            return (string)dpo.GetValue(PasswordProperty);
        }

        /// <summary>Helper for setting <see cref="PasswordProperty"/> on <paramref name="dpo"/>.</summary>
        /// <param name="dpo"><see cref="DependencyObject"/> to set <see cref="PasswordProperty"/> on.</param>
        /// <param name="value">Password property value.</param>
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        public static void SetPassword(DependencyObject dpo, string value)
        {
            dpo.SetValue(PasswordProperty, value);
        }

        /// <summary>
        /// Handles changes to the 'Password' attached property.
        /// </summary>
        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is PasswordBox)
            {
                PasswordBox targetPasswordBox = sender as PasswordBox;
                targetPasswordBox.PasswordChanged -= PasswordBoxPasswordChanged;
                if (!GetIsChanging(targetPasswordBox))
                {
                    targetPasswordBox.Password = (string)e.NewValue;
                }

                targetPasswordBox.PasswordChanged += PasswordBoxPasswordChanged;
            }
        }

        /// <summary>
        /// Handle the 'PasswordChanged'-event on the PasswordBox
        /// </summary>
        private static void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox)
            {
                PasswordBox passwordBox = sender as PasswordBox;
                SetIsChanging(passwordBox, true);
                SetPassword(passwordBox, passwordBox.Password);
                SetIsChanging(passwordBox, false);
            }
        }

        private static void SetRevealedPasswordCaretIndex(PasswordBox passwordBox)
        {
            var textBox = GetRevealedPasswordTextBox(passwordBox);
            if (textBox != null)
            {
                var caretPos = GetPasswordBoxCaretPosition(passwordBox);
                textBox.CaretIndex = caretPos;
            }
        }

        private static int GetPasswordBoxCaretPosition(PasswordBox passwordBox)
        {
            var selection = GetSelection(passwordBox);
            var tTextRange = selection?.GetType().GetInterfaces().FirstOrDefault(i => i.Name == "ITextRange");
            var oStart = tTextRange?.GetProperty("Start")?.GetGetMethod()?.Invoke(selection, null);
            var value = oStart?.GetType().GetProperty("Offset", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(oStart, null) as int?;
            var caretPosition = value.GetValueOrDefault(0);
            return caretPosition;
        }

        private static readonly DependencyProperty IsChangingProperty
            = DependencyProperty.RegisterAttached(
                "IsChanging",
                typeof(bool),
                typeof(PasswordBoxBindingBehavior),
                new UIPropertyMetadata(BooleanBoxes.FalseBox));

        private static bool GetIsChanging(UIElement element)
        {
            return (bool)element.GetValue(IsChangingProperty);
        }

        private static void SetIsChanging(UIElement element, bool value)
        {
            element.SetValue(IsChangingProperty, BooleanBoxes.Box(value));
        }

        private static readonly DependencyProperty SelectionProperty
            = DependencyProperty.RegisterAttached(
                "Selection",
                typeof(TextSelection),
                typeof(PasswordBoxBindingBehavior),
                new UIPropertyMetadata(default(TextSelection)));

        private static TextSelection GetSelection(DependencyObject obj)
        {
            return obj.GetValue(SelectionProperty) as TextSelection;
        }

        private static void SetSelection(DependencyObject obj, TextSelection value)
        {
            obj.SetValue(SelectionProperty, value);
        }

        private static readonly DependencyProperty RevealedPasswordTextBoxProperty
            = DependencyProperty.RegisterAttached(
                "RevealedPasswordTextBox",
                typeof(TextBox),
                typeof(PasswordBoxBindingBehavior),
                new UIPropertyMetadata(default(TextBox)));

        private static TextBox GetRevealedPasswordTextBox(DependencyObject obj)
        {
            return obj.GetValue(RevealedPasswordTextBoxProperty) as TextBox;
        }

        private static void SetRevealedPasswordTextBox(DependencyObject obj, TextBox value)
        {
            obj.SetValue(RevealedPasswordTextBoxProperty, value);
        }
    }
}
