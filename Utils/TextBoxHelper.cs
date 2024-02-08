using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Data;

namespace WpfApplication4
{
    public class TextBoxHelper
    {
        #region HasText
        public static readonly DependencyProperty HasTextProperty
    = DependencyProperty.RegisterAttached(
        "HasText",
        typeof(bool),
        typeof(TextBoxHelper),
        new FrameworkPropertyMetadata(BooleanBoxes.FalseBox, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets if the attached TextBox has text.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        public static bool GetHasText(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasTextProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        public static void SetHasText(DependencyObject obj, bool value)
        {
            obj.SetValue(HasTextProperty, BooleanBoxes.Box(value));
        }
        #endregion

        #region TextLength
        internal static readonly DependencyPropertyKey TextLengthPropertyKey
            = DependencyProperty.RegisterAttachedReadOnly(
                "TextLength",
                typeof(int),
                typeof(TextBoxHelper),
                new PropertyMetadata(0));

        public static readonly DependencyProperty TextLengthProperty = TextLengthPropertyKey.DependencyProperty;

        public static int GetTextLength(UIElement element)
        {
            return (int)element.GetValue(TextLengthProperty);
        }
        #endregion

        #region Watermark
        public static readonly DependencyProperty WatermarkProperty
          = DependencyProperty.RegisterAttached(
              "Watermark",
              typeof(string),
              typeof(TextBoxHelper),
              new UIPropertyMetadata(string.Empty));

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        public static string GetWatermark(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }
        #endregion

        #region ClearTextButton
        public static readonly DependencyProperty ClearTextButtonProperty
            = DependencyProperty.RegisterAttached(
                "ClearTextButton",
                typeof(bool),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(BooleanBoxes.FalseBox, ButtonCommandOrClearTextChanged));

        /// <summary>
        /// Gets the clear text button visibility / feature. Can be used to enable text deletion.
        /// </summary>
        public static bool GetClearTextButton(DependencyObject d)
        {
            return (bool)d.GetValue(ClearTextButtonProperty);
        }

        /// <summary>
        /// Sets the clear text button visibility / feature. Can be used to enable text deletion.
        /// </summary>
        public static void SetClearTextButton(DependencyObject obj, bool value)
        {
            obj.SetValue(ClearTextButtonProperty, BooleanBoxes.Box(value));
        }
        #endregion

        #region IsClearTextButtonBehaviorEnabled
        /// <summary>
        /// The clear text button behavior property. It sets a click event to the button if the value is true.
        /// </summary>
        public static readonly DependencyProperty IsClearTextButtonBehaviorEnabledProperty
            = DependencyProperty.RegisterAttached(
                "IsClearTextButtonBehaviorEnabled",
                typeof(bool),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(BooleanBoxes.FalseBox, IsClearTextButtonBehaviorEnabledChanged));

        /// <summary>
        /// Gets the clear text button behavior.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(ButtonBase))]
        public static bool GetIsClearTextButtonBehaviorEnabled(Button d)
        {
            return (bool)d.GetValue(IsClearTextButtonBehaviorEnabledProperty);
        }

        /// <summary>
        /// Sets the clear text button behavior.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(ButtonBase))]
        public static void SetIsClearTextButtonBehaviorEnabled(Button obj, bool value)
        {
            obj.SetValue(IsClearTextButtonBehaviorEnabledProperty, BooleanBoxes.Box(value));
        }
        #endregion

        #region ButtonCommand
        public static readonly DependencyProperty ButtonCommandProperty
    = DependencyProperty.RegisterAttached(
        "ButtonCommand",
        typeof(ICommand),
        typeof(TextBoxHelper),
        new FrameworkPropertyMetadata(null, ButtonCommandOrClearTextChanged));

        public static ICommand GetButtonCommand(DependencyObject d)
        {
            return d.GetValue(ButtonCommandProperty) as ICommand;
        }

        public static void SetButtonCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ButtonCommandProperty, value);
        }
        #endregion

        #region ButtonCommandParameter
        public static readonly DependencyProperty ButtonCommandParameterProperty
            = DependencyProperty.RegisterAttached(
                "ButtonCommandParameter",
                typeof(object),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(null));

        public static object GetButtonCommandParameter(DependencyObject d)
        {
            return d.GetValue(ButtonCommandParameterProperty);
        }

        public static void SetButtonCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(ButtonCommandParameterProperty, value);
        }
        #endregion

        #region Function
        private static void IsClearTextButtonBehaviorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue && d is Button)
            {
                var button = d as Button;
                button.Click -= ButtonClicked;
                if ((bool)e.NewValue)
                {
                    button.Click += ButtonClicked;
                }
            }
        }

        private static void SetTextLength<TDependencyObject>(TDependencyObject sender, Func<TDependencyObject, int> funcTextLength)
            where TDependencyObject : DependencyObject
        {
            if (sender != null)
            {
                var value = funcTextLength(sender);
                sender.SetValue(TextLengthPropertyKey, value);
                sender.SetCurrentValue(HasTextProperty, BooleanBoxes.Box(value > 0));
            }
        }

        private static void TextChanged(object sender, RoutedEventArgs e)
        {
            SetTextLength(sender as TextBox, textBox => textBox.Text.Length);
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            SetTextLength(sender as PasswordBox, passwordBox => passwordBox.Password.Length);
        }

        public static void ButtonClicked(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            var ancestors = new List<DependencyObject>();
            DependencyObject p = VisualTreeHelper.GetParent(button);
            while (p != null)
            {
                ancestors.Add(p);
                p = VisualTreeHelper.GetParent(p);
            }
            var parent = ancestors.FirstOrDefault(a => a is RichTextBox || a is TextBox || a is PasswordBox || a is ComboBox);
            //var parent = button.GetAncestors().FirstOrDefault(a => a is RichTextBox || a is TextBox || a is PasswordBox || a is ComboBox);
            if (parent == null)
            {
                return;
            }

            var command = GetButtonCommand(parent);
            var commandParameter = GetButtonCommandParameter(parent) ?? parent;
            if (command != null && command.CanExecute(commandParameter))
            {
                if (parent is TextBox)
                {
                    TextBox textBox = parent as TextBox;
                    textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
                }

                command.Execute(commandParameter);
            }

            if (GetClearTextButton(parent))
            {
                if (parent is RichTextBox)
                {
                    RichTextBox richTextBox = parent as RichTextBox;
                    richTextBox.Document?.Blocks?.Clear();
                    richTextBox.Selection?.Select(richTextBox.CaretPosition, richTextBox.CaretPosition);
                }
                else if (parent is TextBox)
                {
                    TextBox textBox = parent as TextBox;
                    textBox.Clear();
                    textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
                }
                else if (parent is PasswordBox)
                {
                    PasswordBox passwordBox = parent as PasswordBox;
                    passwordBox.Clear();
                    passwordBox.GetBindingExpression(PasswordBoxBindingBehavior.PasswordProperty)?.UpdateSource();
                }
                //else if (parent is MultiSelectionComboBox multiSelectionComboBox)
                //{
                //    if (multiSelectionComboBox.HasCustomText)
                //    {
                //        multiSelectionComboBox.ResetEditableText(true);
                //    }
                //    else
                //    {
                //        switch (multiSelectionComboBox.SelectionMode)
                //        {
                //            case SelectionMode.Single:
                //                multiSelectionComboBox.SetCurrentValue(MultiSelectionComboBox.SelectedItemProperty, null);
                //                break;
                //            case SelectionMode.Multiple:
                //            case SelectionMode.Extended:
                //                multiSelectionComboBox.SelectedItems?.Clear();
                //                break;
                //            default:
                //                throw new NotSupportedException("Unknown SelectionMode");
                //        }
                //        multiSelectionComboBox.ResetEditableText(true);
                //    }
                //}
                else if (parent is ComboBox)
                {
                    ComboBox comboBox = parent as ComboBox;
                    if (comboBox.IsEditable)
                    {
                        comboBox.SetCurrentValue(ComboBox.TextProperty, string.Empty);
                        comboBox.GetBindingExpression(ComboBox.TextProperty)?.UpdateSource();
                    }

                    comboBox.SetCurrentValue(ComboBox.SelectedItemProperty, null);
                    comboBox.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateSource();
                }
                //else if (parent is ColorPickerBase colorPicker)
                //{
                //    colorPicker.SetCurrentValue(ColorPickerBase.SelectedColorProperty, null);
                //}
            }
        }

        private static void ButtonCommandOrClearTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox )
            {
                RichTextBox richTextBox = d as RichTextBox;
                richTextBox.Loaded -= RichTextBoxLoaded;
                richTextBox.Loaded += RichTextBoxLoaded;
                if (richTextBox.IsLoaded)
                {
                    RichTextBoxLoaded(richTextBox, new RoutedEventArgs());
                }
            }
            else if (d is TextBox )
            {
                TextBox textBox = d as TextBox;
                // only one loaded event
                textBox.Loaded -= TextChanged;
                textBox.Loaded += TextChanged;
                if (textBox.IsLoaded)
                {
                    TextChanged(textBox, new RoutedEventArgs());
                }
            }
            else if (d is PasswordBox )
            {
                PasswordBox passwordBox = d as PasswordBox;
                // only one loaded event
                passwordBox.Loaded -= PasswordChanged;
                passwordBox.Loaded += PasswordChanged;
                if (passwordBox.IsLoaded)
                {
                    PasswordChanged(passwordBox, new RoutedEventArgs());
                }
            }
            else if (d is ComboBox )
            {
                ComboBox comboBox = d as ComboBox;
                // only one loaded event
                comboBox.Loaded -= ComboBoxLoaded;
                comboBox.Loaded += ComboBoxLoaded;
                if (comboBox.IsLoaded)
                {
                    ComboBoxLoaded(comboBox, new RoutedEventArgs());
                }
            }
        }

        private static void RichTextBoxLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is RichTextBox)
            {
                RichTextBox richTextBox = sender as RichTextBox;
                SetRichTextBoxTextLength(richTextBox);
            }
        }

        private static void SetRichTextBoxTextLength(RichTextBox richTextBox)
        {
            SetTextLength(richTextBox, rtb =>
            {
                var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                var text = textRange.Text;
                var lastIndexOfNewLine = text.LastIndexOf(Environment.NewLine, StringComparison.InvariantCulture);
                if (lastIndexOfNewLine >= 0)
                {
                    text = text.Remove(lastIndexOfNewLine);
                }

                return text.Length;
            });
        }

        private static void ComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox )
            {
                ComboBox comboBox = sender as ComboBox;
                comboBox.SetCurrentValue(HasTextProperty, BooleanBoxes.Box(!string.IsNullOrWhiteSpace(comboBox.Text) || comboBox.SelectedItem != null));
            }
        }
        #endregion
    }
}
