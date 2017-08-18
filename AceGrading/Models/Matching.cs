using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace AceGrading
{
    public class Matching : Question
    {
        public Matching() { }
        public Matching(Test _ParentTest)
        {
            this.ParentTest = _ParentTest;
            this.TestSection = this.ParentTest.RequiredSection;
            Answer = new List<string>();
            AnswersToPickFrom = new ObservableCollection<MatchingAnswer_UI>();
            OptionalAnswers = new ObservableCollection<MatchingAnswer_UI>();
            InitializeAnswerToPickFrom();
        }

        //Public Attributes
        public List<string> Answer;
        public ObservableCollection<MatchingAnswer_UI> OptionalAnswers { get; set; }
        public ObservableCollection<MatchingAnswer_UI> AnswersToPickFrom { get; set; }
        public QuestionLayoutPreset_UI SelectedPreset
        {
            get { return _SelectedPreset; }
            set
            {
                if (value != _SelectedPreset)
                {
                    _SelectedPreset = value;
                    OnPropertyChanged("SelectedPreset");
                    if (_SelectedPreset != null)
                        CopyAnswersFromPreset();
                }
            }
        }
        public WordBox SelectedWordBox
        {
            get { return _SelectedWordBox; }
            set
            {
                if (value != _SelectedWordBox)
                {
                    //Remove this question from the old word Box
                    if (_SelectedWordBox != null)
                        _SelectedWordBox.RemoveQuestion(this);

                    _SelectedWordBox = value;

                    //Add this question to the new word box
                    if (_SelectedWordBox != null)
                    {
                        _SelectedWordBox.AddQuestion(this);
                        this.CopyAnswersFromWordBox();
                    }
                        

                    OnPropertyChanged("SelectedWordBox");
                }
            }
        }

        //Public Methods
        public void Save_Matching(int index, string answer)
        {
            //Notes: index and Answer.Count are base 1

            //Checks if an answer should be removed
            //**Conditions for Removal: the index must be within range and the answer will be either "" or null
            if (answer.Equals("") || answer.Equals(null))
                Answer.RemoveAt(index - 1);

            //Adds or Replaces the Answer
            if (index > Answer.Count)
                Answer.Add(answer);
            else
                Answer[index - 1] = answer;
        }
        public void SetSelectedPresetToNull()
        {
            this.SelectedPreset = null;
        }
        public char[] GetOptionalAnswers()
        {
            List<char> OptionalAnswersArray = new List<char>();
            for (int i = 0; i < this.OptionalAnswers.Count; i++)
                OptionalAnswersArray.Add(this.OptionalAnswers[i].Letter);

            return OptionalAnswersArray.ToArray();
        }

        //Private Methods
        private void InitializeAnswerToPickFrom()
        {
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'A', isOptionalAnswer = true, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'B', isOptionalAnswer = true, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'C', isOptionalAnswer = true, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'D', isOptionalAnswer = true, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'E', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'F', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'G', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'H', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'I', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'J', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'K', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'L', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'M', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'N', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'O', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'P', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'Q', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'R', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'S', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'T', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'U', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'V', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'W', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'X', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'Y', isOptionalAnswer = false, isAnswer = false });
            AnswersToPickFrom.Add(new MatchingAnswer_UI(this) { Letter = 'Z', isOptionalAnswer = false, isAnswer = false });
        }
        private void CopyAnswersFromPreset()
        {
            if (SelectedPreset != null)
            {
                if (SelectedPreset.NumberOfLetters > 0)
                {
                    List<char> NewLetters = new List<char>(SelectedPreset.Letters);

                    //Remove the old preset
                    for (int i = 0; i < this.OptionalAnswers.Count; )
                        this.OptionalAnswers[i].isOptionalAnswer = false;

                    //Add all of the letters from the preset to this answer
                    foreach (char Letter in NewLetters)
                        this.AnswersToPickFrom[Letter - 'A'].isOptionalAnswer = true;
                }
            }
        }
        private void CopyAnswersFromWordBox()
        {
            if (SelectedWordBox != null)
            {
                if (SelectedWordBox.NumberOfOptions > 0)
                {
                    //Remove any letters that are currently there
                    for (int i = 0; i < this.OptionalAnswers.Count;)
                        this.OptionalAnswers[i].isOptionalAnswer = false;

                    //Add all the letters from the Wordbox to this answer
                    for (int i = 0; i < this.SelectedWordBox.OptionalAnswers.Count; i++)
                        this.AnswersToPickFrom[this.SelectedWordBox.OptionalAnswers[i].Letter - 'A'].isOptionalAnswer = true;
                }
            }
        }

        //Private Variables
        private QuestionLayoutPreset_UI _SelectedPreset;
        private WordBox _SelectedWordBox;
    }

    public class MatchingAnswer_UI : INotifyPropertyChanged
    {
        public MatchingAnswer_UI(Matching _parentQuestion)
        {
            parentQuestion = _parentQuestion;
        }
        public MatchingAnswer_UI() { }

        //Public Attributes
        public Matching parentQuestion { get; set; }
        public char Letter { get; set; }
        public bool isOptionalAnswer
        {
            get { return _isOptionalAnswer; }
            set
            {
                if (value != _isOptionalAnswer)
                {
                    //Update the parent list
                    if (value)
                    {
                        //Save the previous sequence, add the new letter, and then update the presets based on the new sequence
                        char[] PrevLetters = parentQuestion.GetOptionalAnswers();
                        AddAnswertoList();
                        parentQuestion.ParentTest.LayoutPresets.AddOrUpdateMatchingPreset(PrevLetters, parentQuestion.GetOptionalAnswers());
                    }
                    else
                    {
                        //Save the previous sequence, remove the letter, and then update the presets based on the new sequence
                        char[] PrevLetters = parentQuestion.GetOptionalAnswers();
                        RemoveAnswerfromList();
                        parentQuestion.ParentTest.LayoutPresets.AddOrUpdateMatchingPreset(PrevLetters, parentQuestion.GetOptionalAnswers());
                    }
                        
                    _isOptionalAnswer = value;
                    OnPropertyChanged("isOptionalAnswer");

                    //Get rid of the preset since it was changed
                    parentQuestion.SetSelectedPresetToNull();
                }
            }
        }
        public bool isAnswer
        {
            get { return _isAnswer; }
            set
            {
                if (value != _isAnswer)
                    _isAnswer = !_isAnswer;
                OnPropertyChanged("isAnswer");
            }
        }


        //Private Methods
        private void AddAnswertoList()
        {
            //Handle the base case
            if (parentQuestion.OptionalAnswers.Count == 0)
            {
                parentQuestion.OptionalAnswers.Add(this);
                return;
            }
                
            //Ensure that the letter is inserted in alphabetical order
            for (int i = 0; i < parentQuestion.OptionalAnswers.Count; i++)
            {
                if (parentQuestion.OptionalAnswers[i].Letter > this.Letter)
                {
                    parentQuestion.OptionalAnswers.Insert(i, this);
                    break;
                }
                else if (i + 1 == parentQuestion.OptionalAnswers.Count)
                {
                    parentQuestion.OptionalAnswers.Add(this);
                    break;
                }   
            }
        }
        private void RemoveAnswerfromList()
        {
            this.isAnswer = false;
            parentQuestion.OptionalAnswers.Remove(this);
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Private variables
        private bool _isAnswer, _isOptionalAnswer;
    }
}
