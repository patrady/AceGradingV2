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
    /// Interaction logic for TimePicker.xaml
    /// </summary>
    public partial class TimePicker : UserControl
    {
        public TimePicker()
        {
            InitializeComponent();
        }

        public DateTime Time
        {
            get { return (DateTime)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(DateTime), typeof(TimePicker), new UIPropertyMetadata(DateTime.Now));

        public bool LimitTo24Hour
        {
            get { return (bool)GetValue(LimitTo24HourProperty); }
            set { SetValue(LimitTo24HourProperty, value); }
        }

        public static readonly DependencyProperty LimitTo24HourProperty =
            DependencyProperty.Register("LimitTo24Hour", typeof(bool), typeof(TimePicker), new PropertyMetadata(false));



        //New Dependency Properties


        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(TimePicker), new PropertyMetadata(string.Empty));


        public enum _LabelPosition { Left, Top, Right, Bottom }
        public _LabelPosition LabelPosition
        {
            get { return (_LabelPosition)GetValue(LabelPositionProperty); }
            set { SetValue(LabelPositionProperty, value); }
        }
        public static readonly DependencyProperty LabelPositionProperty =
            DependencyProperty.Register("LabelPosition", typeof(_LabelPosition), typeof(TimePicker), new PropertyMetadata(_LabelPosition.Right));


        public enum _Notation { TwentyFourHour, TwelveHour }
        public _Notation Notation
        {
            get { return (_Notation)GetValue(NotationProperty); }
            set { SetValue(NotationProperty, value); }
        }
        public static readonly DependencyProperty NotationProperty =
            DependencyProperty.Register("Notation", typeof(_Notation), typeof(TimePicker), new PropertyMetadata(_Notation.TwelveHour));


        public bool ShowDate
        {
            get { return (bool)GetValue(ShowDateProperty); }
            set { SetValue(ShowDateProperty, value); }
        }
        public static readonly DependencyProperty ShowDateProperty =
            DependencyProperty.Register("ShowDate", typeof(bool), typeof(TimePicker), new PropertyMetadata(false));


        public bool ShowSeconds
        {
            get { return (bool)GetValue(ShowSecondsProperty); }
            set { SetValue(ShowSecondsProperty, value); }
        }
        public static readonly DependencyProperty ShowSecondsProperty =
            DependencyProperty.Register("ShowSeconds", typeof(bool), typeof(TimePicker), new PropertyMetadata(false));


        public new SolidColorBrush Foreground
        {
            get { return (SolidColorBrush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        public static readonly new DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(SolidColorBrush), typeof(TimePicker), new PropertyMetadata(new SolidColorBrush(Colors.Black)));



        public double LabelWidth
        {
            get { return (double)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }

        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", typeof(double), typeof(TimePicker), new PropertyMetadata(double.MaxValue));






        private void UpHour()
        {
            this.Time = this.Time.AddHours(1);
            if (this.LimitTo24Hour)
                CheckIncrementTime();
        }
        private void DownHour()
        {
            this.Time = this.Time.AddHours(-1);
            if (this.LimitTo24Hour)
                CheckDecrementTime();
        }
        private void UpMinute()
        {
            this.Time = this.Time.AddMinutes(1);
            if (this.LimitTo24Hour)
                CheckIncrementTime();
        }
        private void DownMinute()
        {
            this.Time = this.Time.AddMinutes(-1);
            if (this.LimitTo24Hour)
                CheckDecrementTime();
        }
        private void TimeIncrementAMPM()
        {
            //this.Time = this.Time.AddHours(12);
            //if (this.LimitTo24Hour)
            //    CheckIncrementTime();
        }
        private void TimeDecrementAMPM()
        {
            //this.Time.TimeOfDay
            //this.Time = this.Time.AddHours(-12);
            //if (this.LimitTo24Hour)
            //    CheckDecrementTime();
        }
        private void UpDate()
        {
            this.Time = this.Time.AddDays(1);
        }
        private void DownDate()
        {
            this.Time = this.Time.AddDays(-1);
        }
        private void UpMonth()
        {
            this.Time = this.Time.AddMonths(1);
        }
        private void DownMonth()
        {
            this.Time = this.Time.AddMonths(-1);
        }
        private void CheckDecrementTime()
        {
            if (this.Time < DateTime.Now)
                this.Time = this.Time.AddHours(24);
        }
        private void CheckIncrementTime()
        {
            if (this.Time > DateTime.Now.AddHours(24))
                this.Time = this.Time.AddHours(-24);
        }
        private void CheckIfDateIsInMonth()
        {
            
        }

        private void UpHourClick(object sender, RoutedEventArgs e)
        {
            UpHour();
        }
        private void DownHourClick(object sender, RoutedEventArgs e)
        {
            DownHour();
        }
        private void UpMinuteClick(object sender, RoutedEventArgs e)
        {
            UpMinute();
        }
        private void DownMinuteClick(object sender, RoutedEventArgs e)
        {
            DownMinute();
        }
        private void AMClick(object sender, RoutedEventArgs e)
        {
            TimeIncrementAMPM();
        }
        private void PMClick(object sender, RoutedEventArgs e)
        {
            TimeDecrementAMPM();
        }

        private void UpMonthClick(object sender, RoutedEventArgs e)
        {
            UpMonth();
        }
        private void DownMonthClick(object sender, RoutedEventArgs e)
        {
            DownMonth();
        }
        private void UpDateClick(object sender, RoutedEventArgs e)
        {
            UpDate();
        }
        private void DownDateClick(object sender, RoutedEventArgs e)
        {
            DownDate();
        }

    }
}
