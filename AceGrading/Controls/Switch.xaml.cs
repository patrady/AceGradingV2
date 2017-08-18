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
    public partial class Switch : UserControl
    {
        public Switch()
        {
            InitializeComponent();
        }


        public bool IsLeftChecked
        {
            get { return (bool)GetValue(IsLeftCheckedProperty); }
            set { SetValue(IsLeftCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsLeftCheckedProperty =
            DependencyProperty.Register("IsLeftChecked", typeof(bool), typeof(Switch), new PropertyMetadata(true));


        public bool IsRightChecked
        {
            get { return (bool)GetValue(IsRightCheckedProperty); }
            set { SetValue(IsRightCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsRightCheckedProperty =
            DependencyProperty.Register("IsRightChecked", typeof(bool), typeof(Switch), new PropertyMetadata(false));


        public string LeftLabel
        {
            get { return (string)GetValue(LeftLabelProperty); }
            set { SetValue(LeftLabelProperty, value); }
        }

        public static readonly DependencyProperty LeftLabelProperty =
            DependencyProperty.Register("LeftLabel", typeof(string), typeof(Switch), new PropertyMetadata(string.Empty));


        public string RightLabel
        {
            get { return (string)GetValue(RightLabelProperty); }
            set { SetValue(RightLabelProperty, value); }
        }

        public static readonly DependencyProperty RightLabelProperty =
            DependencyProperty.Register("RightLabel", typeof(string), typeof(Switch), new PropertyMetadata(string.Empty));







    }
}
