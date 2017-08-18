using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AceGrading
{
    /// <summary>
    /// Interaction logic for TextBoxCustomUIHelper.xaml
    /// </summary>
    public partial class TextBoxCustom : UserControl
    {
        public TextBoxCustom()
        {
            InitializeComponent();
        }



        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(TextBoxCustom), new PropertyMetadata(string.Empty));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxCustom), new PropertyMetadata(string.Empty));


        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(TextBoxCustom), new PropertyMetadata(TextAlignment.Left));


        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(TextBoxCustom), new PropertyMetadata(int.MaxValue));


        public enum _AllowOnly { Integer, Integer_Positive, Integer_Negative, Decimal, Decimal_Negative, Decimal_Positive, Numeric, Letters, All };
        public _AllowOnly AllowOnly
        {
            get { return (_AllowOnly)GetValue(AllowOnlyProperty); }
            set { SetValue(AllowOnlyProperty, value); }
        }

        public static readonly DependencyProperty AllowOnlyProperty =
            DependencyProperty.Register("AllowOnly", typeof(_AllowOnly), typeof(TextBoxCustom), new PropertyMetadata(_AllowOnly.All));






        //Methods

        private void Txtbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            int intParsed;
            double doubleParsed;

            switch (this.AllowOnly)
            {
                case _AllowOnly.Integer:
                    return int.TryParse(text, out intParsed);
                    
                case _AllowOnly.Integer_Negative:
                    if (int.TryParse(text, out intParsed))
                        return intParsed < 0;
                    return false;

                case _AllowOnly.Integer_Positive:
                    if (int.TryParse(text, out intParsed))
                        return intParsed >= 0;
                    return false;

                case _AllowOnly.Numeric:
                case _AllowOnly.Decimal:
                    return double.TryParse(text, out doubleParsed);

                case _AllowOnly.Decimal_Negative:
                    if (double.TryParse(text, out doubleParsed))
                        return doubleParsed < 0;
                    return false;

                case _AllowOnly.Decimal_Positive:
                    if (double.TryParse(text, out doubleParsed))
                        return doubleParsed >= 0;
                    return false;

                case _AllowOnly.Letters:
                    Regex regExpression = new Regex("[a-zA-Z]");
                    return !regExpression.IsMatch(text);

                case _AllowOnly.All:
                default:
                    return true;
            }
        }

        private void Txtbox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                if (!IsTextAllowed((string) e.DataObject.GetData(typeof(string))))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }
    }
}
