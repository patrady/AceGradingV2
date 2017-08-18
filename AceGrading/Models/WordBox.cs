using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AceGrading
{
    public class WordBox : INotifyPropertyChanged
    {
        public WordBox(Test _ParentTest)
        {
            ParentTest = _ParentTest;
            Questions = new ObservableCollection<Question>();
            this.OptionalAnswers = new ObservableCollection<WordBoxLetter_UI>();
            IsNameDefaultName = true;
            Index = 0;
            InitializeAnswerToPickFrom();
        }
        public WordBox()
        {
            ParentTest = ParentTest;
            Questions = new ObservableCollection<Question>();
            this.OptionalAnswers = new ObservableCollection<WordBoxLetter_UI>();
            IsNameDefaultName = true;
            Index = 0;
            InitializeAnswerToPickFrom();
        }

        //Public Attributes
        public ObservableCollection<Question> Questions { get; set; }
        public List<WordBoxLetter_UI> AnswersToPickFrom { get; set; }
        public ObservableCollection<WordBoxLetter_UI> OptionalAnswers { get; set; }
        public int NumberOfOptions { get { return this.OptionalAnswers.Count; } }
        public int Index
        {
            get { return _Index; }
            set
            {
                if (value != _Index)
                {
                    _Index = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public object Database_ID { get; set; }
        public string Name
        {
            get
            {
                if (IsNameDefaultName)
                    return this.DefaultName;
                return _Name;
            }
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    OnPropertyChanged("Name");
                    IsNameDefaultName = false;
                }
            }
        }
        public Test ParentTest { get; set; }

        //Public Methods
        public void AddQuestion(Question question)
        {
            this.Questions.Add(question);
        }
        public void RemoveQuestion(Question question)
        {
            this.Questions.Remove(question);
        }

        //Private Attributes
        private string DefaultName{ get{ return "Word Box #" + (this.Index + 1); } }
        private void InitializeAnswerToPickFrom()
        {
            this.AnswersToPickFrom = new List<WordBoxLetter_UI>();
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'A', isOptionalAnswer = true });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'B', isOptionalAnswer = true });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'C', isOptionalAnswer = true });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'D', isOptionalAnswer = true });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'E', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'F', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'G', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'H', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'I', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'J', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'K', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'L', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'M', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'N', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'O', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'P', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'Q', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'R', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'S', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'T', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'U', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'V', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'W', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'X', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'Y', isOptionalAnswer = false });
            AnswersToPickFrom.Add(new WordBoxLetter_UI(this) { Letter = 'Z', isOptionalAnswer = false });
        }

        //Private variables
        private int _Index;
        private string _Name;
        private bool IsNameDefaultName;

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class WordBoxLetter_UI
    {
        public WordBoxLetter_UI(WordBox _ParentWordBox)
        {
            this.SubAnswerPrompt = null;
            this.ParentWordBox = _ParentWordBox;
        }
        public WordBoxLetter_UI() { }

        //Public Attributes
        public WordBox ParentWordBox { get; set; }
        public char Letter { get; set; }
        public string SubAnswerPrompt
        {
            get { return _SubAnswerPrompt; }
            set
            {
                if (value != _SubAnswerPrompt)
                {
                    _SubAnswerPrompt = value;
                    OnPropertyChanged("SubAnswerPrompt");
                }
            }
        }
        public bool isOptionalAnswer
        {
            get { return _isOptionalAnswer; }
            set
            {
                if (value != _isOptionalAnswer)
                {
                    _isOptionalAnswer = value;
                    
                    //Update the parent list
                    if (value)
                    {
                        AddAnswertoList();
                    }
                    else
                    {
                        RemoveAnswerfromList();
                    }

                    OnPropertyChanged("isOptionalAnswer");
                }
            }
        }

        //Private Methods
        private void AddAnswertoList()
        {
            //Handle the base case
            if (ParentWordBox.OptionalAnswers.Count == 0)
            {
                ParentWordBox.OptionalAnswers.Add(this);
                return;
            }

            //Ensure that the letter is inserted in alphabetical order
            int sortedIndex = 0;
            for (int i = 0; i < ParentWordBox.OptionalAnswers.Count; i++)
            {
                if (ParentWordBox.OptionalAnswers[i].Letter > this.Letter)
                {
                    ParentWordBox.OptionalAnswers.Insert(i, this);
                    sortedIndex = i;
                    break;
                }
                else if (i + 1 == ParentWordBox.OptionalAnswers.Count)
                {
                    ParentWordBox.OptionalAnswers.Add(this);
                    sortedIndex = i + 1;
                    break;
                }
            }

            //Add this new answer to all questions that reference this word box
            foreach (Question question in this.ParentWordBox.Questions)
                if (question is Matching)
                    (question as Matching).OptionalAnswers.Insert(sortedIndex, (question as Matching).AnswersToPickFrom[this.Letter - 'A']);
        }
        private void RemoveAnswerfromList()
        {
            this.SubAnswerPrompt = null;
            ParentWordBox.OptionalAnswers.Remove(this);

            //Remove this answer from all questions that reference this word box
            foreach (Question question in this.ParentWordBox.Questions)
                if (question is Matching)
                    (question as Matching).OptionalAnswers.Remove((question as Matching).AnswersToPickFrom[this.Letter - 'A']);
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Private variables
        private string _SubAnswerPrompt;
        private bool _isOptionalAnswer;
    }
}
