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
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : UserControl
    {
        public ConfirmationWindow()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ConfirmationWindow), new PropertyMetadata(string.Empty));


        public enum ConfirmationWindowButtonChoices { YesNo, YesNoCancel, Continue, YesCancel }
        public ConfirmationWindowButtonChoices ButtonOptions
        {
            get { return (ConfirmationWindowButtonChoices)GetValue(ButtonOptionsProperty); }
            set { SetValue(ButtonOptionsProperty, value); }
        }

        public static readonly DependencyProperty ButtonOptionsProperty =
            DependencyProperty.Register("ButtonOptions", typeof(ConfirmationWindowButtonChoices), typeof(ConfirmationWindow), new PropertyMetadata(ConfirmationWindowButtonChoices.YesCancel));


        public enum ConfirmationWindowType { Error, Warning, Good }
        public ConfirmationWindowType Type
        {
            get { return (ConfirmationWindowType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(ConfirmationWindowType), typeof(ConfirmationWindow), new PropertyMetadata(ConfirmationWindowType.Good));
    }



}
