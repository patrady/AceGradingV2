using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for TextBoxWithTextBlockCustomUIHelper.xaml
    /// </summary>
    public partial class TextBoxWithTextBlockCustom : UserControl
    {
        public TextBoxWithTextBlockCustom()
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
            DependencyProperty.Register("Placeholder", typeof(string), typeof(TextBoxWithTextBlockCustom), new PropertyMetadata(string.Empty));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxWithTextBlockCustom), new PropertyMetadata(string.Empty));



        public double NewFontSize
        {
            get { return (double)GetValue(NewFontSizeProperty); }
            set { SetValue(NewFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewFontSizeProperty =
            DependencyProperty.Register("NewFontSize", typeof(double), typeof(TextBoxWithTextBlockCustom), new PropertyMetadata(16.0));



    }
}
