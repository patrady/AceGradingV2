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
    public partial class LetterSelector : UserControl
    {
        public LetterSelector()
        {
            InitializeComponent();
        }

        public _SelectionMode SelectionMode
        {
            get { return (_SelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectionMode", typeof(_SelectionMode), typeof(LetterSelector), new PropertyMetadata(_SelectionMode.Single));



        public List<Letter> Letters
        {
            get { return (List<Letter>)GetValue(LettersProperty); }
            set { SetValue(LettersProperty, value); }
        }

        public static readonly DependencyProperty LettersProperty =
            DependencyProperty.Register("Letters", typeof(List<Letter>), typeof(LetterSelector), new PropertyMetadata(new List<Letter>()
            {
                new Letter('A'), new Letter('B'), new Letter('C'), new Letter('D'), new Letter('E'), new Letter('F'),
                new Letter('G'), new Letter('H'), new Letter('I'), new Letter('J'), new Letter('K'), new Letter('L'),
                new Letter('M'), new Letter('N'), new Letter('O'), new Letter('P'), new Letter('Q'), new Letter('R'),
                new Letter('S'), new Letter('T'), new Letter('U'), new Letter('V'), new Letter('W'), new Letter('X'),
                new Letter('Y'), new Letter('Z'),
            }));

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
