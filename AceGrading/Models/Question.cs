﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace AceGrading
{
    public class Question : INotifyPropertyChanged
    {
        public Question()
        {
            this.Number = 0;
            this.Correctly_Answered = 0;
        }

        //Public Attributes
        public Test ParentTest
        {
            get { return _Test; }
            set
            {
                _Test = value;
                OnPropertyChanged("ParentTest");
            }
        }
        public int Number
        {
            get { return _Number; }
            set
            {
                if (value != _Number)
                {
                    _Number = value;
                    OnPropertyChanged("Number");
                }
            }
        }
        public int Correctly_Answered { get; set; }
        public double Point_Value
        {
            get { return _PointValue; }
            set
            {
                if (value != _PointValue)
                {
                    double PrevValue = _PointValue;
                    _PointValue = value;
                    this.ParentTest.UpdatePointsRemaining(PrevValue, value);
                    OnPropertyChanged("Point_Value");
                }
            }
        }
        public Section TestSection
        {
            get { return _TestSection; }
            set
            {
                if (value != null)
                {
                    if (_TestSection == null || value != _TestSection)
                    {
                        this.ParentTest.SwitchQuestionTestSection(this, _TestSection, value);
                        _TestSection = value;
                        OnPropertyChanged("TestSection");
                    }
                }
            }
        }
        public object Database_ID { get; set; }
        public string QuestionPrompt
        {
            get { return _QuestionPrompt; }
            set
            {
                if (value != _QuestionPrompt)
                {
                    _QuestionPrompt = value;
                    OnPropertyChanged("QuestionPrompt");
                }
            }
        }

        //Public Commands
        public DeleteQuestion_Command DeleteQuestion { get; set; }
        public Switch_Command Switch { get; set; }

        //Public Methods
        public void DecrementNumber()
        {
            this.Number--;
        }
        public void SetParentTest(Test _ParentTest)
        {
            this.ParentTest = _ParentTest;

            //Update Commands
            DeleteQuestion = new DeleteQuestion_Command(this.ParentTest);
            Switch = new Switch_Command(this.ParentTest);
        }

        //Private Variables
        private Section _TestSection;
        private int _Number;
        private double _PointValue;
        private Test _Test;
        private string _QuestionPrompt;

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public class DeleteQuestion_Command : ICommand
    {
        Test test;
        public event EventHandler CanExecuteChanged;

        public DeleteQuestion_Command(Test _Test) { test = _Test; }

        public bool CanExecute(object parameter) { return true; }

        public void Execute(object parameter)
        {
            //Decrement all question numbers that fall after this question
            for (int i = test.SelectedQuestion.Number; i < test.Answers.Count; i++)
                test.Answers[i].DecrementNumber();

            //Remove the Question from its section
            test.SelectedQuestion.TestSection.RemoveQuestion(test.SelectedQuestion);

            if (test.SelectedQuestion is Matching)
            {
                //Delete the Layout from storage if it is a Matching Question
                test.LayoutPresets.AddOrUpdateMatchingPreset((test.SelectedQuestion as Matching).GetOptionalAnswers(), null);

                //Remove the question from the Word Boxes if it has one
                if ((test.SelectedQuestion as Matching).SelectedWordBox != null)
                    (test.SelectedQuestion as Matching).SelectedWordBox.RemoveQuestion(test.SelectedQuestion as Matching);
            }
                

            //Delete the question
            test.Answers.Remove(test.SelectedQuestion);
        }
    }

    public class Switch_Command : ICommand
    {
        Test test;
        public event EventHandler CanExecuteChanged;

        public Switch_Command(Test _Test) { test = _Test; }

        public bool CanExecute(object parameter) { return true; }

        public void Execute(object parameter)
        {
            test.SelectedQuestion.TestSection = test.RequiredSection;
        }
    }
}
