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
    /// Interaction logic for TestQuestions.xaml
    /// </summary>
    public partial class TestQuestions : UserControl
    {
        public TestQuestions()
        {
            InitializeComponent();
        }
    }

    public class ComboBoxQuestionType
    {
        public ComboBoxQuestionType() { }
        public string Type { get; set; }
        public string ImageSource { get; set; }
    }

    public class CheckBoxQuestion { }
}
