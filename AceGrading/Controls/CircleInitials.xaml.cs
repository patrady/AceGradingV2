using System.Windows;
using System.Windows.Controls;

namespace AceGrading
{
    public partial class CircleInitials : UserControl
    {
        public CircleInitials()
        {
            InitializeComponent();
        }



        public string Initials
        {
            get { return (string)GetValue(InitialsProperty); }
            set { SetValue(InitialsProperty, value); }
        }

        public static readonly DependencyProperty InitialsProperty =
            DependencyProperty.Register("Initials", typeof(string), typeof(CircleInitials), new PropertyMetadata("AA"));

        public enum ClassStudentObjectType { Student, Class, Test };
        public ClassStudentObjectType Type
        {
            get { return (ClassStudentObjectType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(ClassStudentObjectType), typeof(CircleInitials), new PropertyMetadata(ClassStudentObjectType.Student));



        public _Size Size
        {
            get { return (_Size)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Size.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(_Size), typeof(CircleInitials), new PropertyMetadata(_Size.Large));


    }
}
