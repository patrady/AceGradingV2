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
    public partial class NumberCarousel : UserControl
    {
        public NumberCarousel()
        {
            InitializeComponent();
        }

        public enum _Orientation { Vertical, Horizontal }
        public _Orientation Orientation
        {
            get { return (_Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(_Orientation), typeof(NumberCarousel), new PropertyMetadata(_Orientation.Vertical));


        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(int), typeof(NumberCarousel), new PropertyMetadata(100));



        public int MaxNumber
        {
            get { return (int)GetValue(MaxNumberProperty); }
            set { SetValue(MaxNumberProperty, value); }
        }

        public static readonly DependencyProperty MaxNumberProperty =
            DependencyProperty.Register("MaxNumber", typeof(int), typeof(NumberCarousel), new PropertyMetadata(int.MaxValue));



        public int MinNumber
        {
            get { return (int)GetValue(MinNumberProperty); }
            set { SetValue(MinNumberProperty, value); }
        }

        public static readonly DependencyProperty MinNumberProperty =
            DependencyProperty.Register("MinNumber", typeof(int), typeof(NumberCarousel), new PropertyMetadata(0));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(NumberCarousel), new PropertyMetadata(string.Empty));



        public _Size Size
        {
            get { return (_Size)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(_Size), typeof(NumberCarousel), new PropertyMetadata(_Size.Large));




        public bool Cycle
        {
            get { return (bool)GetValue(CycleProperty); }
            set { SetValue(CycleProperty, value); }
        }

        public static readonly DependencyProperty CycleProperty =
            DependencyProperty.Register("Cycle", typeof(bool), typeof(NumberCarousel), new PropertyMetadata(false));



        public double LabelWidth
        {
            get { return (double)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", typeof(double), typeof(NumberCarousel), new PropertyMetadata(double.MaxValue));







        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            if (Cycle)
                Number = (Number + 1) % (MaxNumber + 1);
            else
                Number = (Number.Equals(MaxNumber) ? MaxNumber : Number + 1);
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (Cycle)
                Number = (Number.Equals(MinNumber) ? MaxNumber : Number - 1);
            else
                Number = (Number.Equals(MinNumber) ? MinNumber : Number - 1);
        }
    }
}
