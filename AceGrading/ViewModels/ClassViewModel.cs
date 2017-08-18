﻿using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AceGrading
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Classes = new ObservableCollection<Class>();

            Class class1 = new Class();
            class1.Class_Name = "Theology IV";
            class1.Add_Student(new Student() { Name = "Robert Brady" });
            class1.Add_Student(new Student() { Name = "Julie Brady" });
            class1.Add_Student(new Student() { Name = "Kristen Duke" });
            class1.Add_Student(new Student() { Name = "Joe Cloud" });
            class1.Add_Student(new Student() { Name = "Nick Nocholi" });
            class1.Add_Test(new Test() { TestName = "Test #1", Upload_File_Name = @"C:\Users\rober\Desktop\pingpong.png", Point_Worth = 100, Statistics = new Test_Statistics(), Is_Graded = true, HighestScore = 99, ParentClass = class1 });
            class1.Add_Test(new Test() { TestName = "Test #2", Upload_File_Name = @"C:\Users\rober\Desktop\Doc1.docx", Point_Worth = 200, Statistics = new Test_Statistics(), Is_Graded = true, HighestScore = 98, ParentClass = class1 });

            Class class2 = new Class();
            class2.Class_Name = "History / Geography";
            class2.Add_Student(new Student() { Name = "Joseph Herring"});
            class2.Add_Student(new Student() { Name = "Alberto Rudeo"});
            class2.Add_Student(new Student() { Name = "Laura Cook"});
            class2.Add_Student(new Student() { Name = "PJ Biyani"});
            class2.Add_Student(new Student() { Name = "Kartik Gupta"});
            class2.Add_Test(new Test() { TestName = "Test #3", Statistics = new Test_Statistics(), Is_Graded = true, HighestScore = 97, ParentClass = class2 });
            class2.Add_Test(new Test() { TestName = "Test #4", Statistics = new Test_Statistics(), Is_Graded = true, HighestScore = 96, ParentClass = class2 });

            this.Add_Class(class1);
            this.Add_Class(class2);
            this.DeleteClass = new DeleteClass_Command(this);
            this.AddClass = new AddClass_Command(this);
            this.PreAddClass = new PreAddClass_Command(this);
            NewClass = new Class();
            SelectedClass = this.Classes.ElementAt(0);
            SelectedClass.SelectedTest = SelectedClass.Tests.ElementAt(0);
            this.AverageTestScore = 73.0;
            this.TestsTaken = 5;
            this.LastTestScore = 93.6;
            this.ClassRank = 4;
        }

        //Fake Data for the LiveCharts
        public double AverageTestScore { get; set; }
        public double TestsTaken { get; set; }
        public double LastTestScore { get; set; }
        public double ClassRank { get; set; }

        //Attributes
        public ObservableCollection<Class> Classes { get; set; }
        public Class NewClass { get; set; }
        public Class SelectedClass
        {
            get { return _SelectedClass; }
            set
            {
                if (value != _SelectedClass)
                {
                    _SelectedClass = value;
                    OnPropertyChanged("SelectedClass");
                }
            }
        }
        public bool HasClasses
        {
            get { return _HasClasses; }
            set
            {
                if (value != _HasClasses)
                {
                    _HasClasses = value;
                    OnPropertyChanged("HasClasses");
                }
            }
        }

        //Methods
        public ReturnValidation Add_Class(Class _class)
        {
            //Check if empty
            if (_class.Class_Name == null || _class.Class_Name == "")
                return new ReturnValidation(_IsOk: false, _Header: "Add Class", _Body: "A class must receive a valid name.");

            //Check if the test name is already taken
            foreach (Class tempClass in this.Classes)
                if (tempClass.Class_Name.ToLower() == _class.Class_Name.ToLower())
                    return new ReturnValidation(_IsOk: false, _Header: "Add Class", _Body: "A class by this name already exists, please choose another name.");

            //Add the test since no other student has the name
            this.Classes.Add(_class);
            this.HasClasses = true;
            return new ReturnValidation(_IsOk: true);
        }
        public ReturnValidation ReName_Class(Class _class, string NewName)
        {
            //Check if empty
            if (_class.Class_Name == null || _class.Class_Name == "")
                return new ReturnValidation(_IsOk: false, _Header: "Rename Class", _Body: "A class must receive a valid name.");

            //Check if the student name matches any other student names
            foreach (Class tempClass in this.Classes)
                if (NewName.ToLower() == tempClass.Class_Name.ToLower())
                    if (tempClass != _class)
                        return new ReturnValidation(_IsOk: false, _Header: "Rename Class", _Body: "A class by this name already exists.");

            _class.Class_Name = NewName;
            return new ReturnValidation(_IsOk: true);
        }
        public void Delete_Class(Class _class)
        {
            this.Classes.Remove(_class);

            if (this.Classes.Count == 0)
                this.HasClasses = false;
        }
        public void ChangeSelectedClass()
        {
            if (this.HasClasses)
                this.SelectedClass = this.Classes[0];
        }
        public bool IsClassInList(Class _class) 
        {
            return this.Classes.Contains(_class);
        }

        //Commands
        public PreAddClass_Command PreAddClass { get; set; }
        public AddClass_Command AddClass { get; set; }
        public DeleteClass_Command DeleteClass { get; set; }

        //Private Variables
        private Class _SelectedClass;
        private bool _HasClasses;

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

    public class Student : INotifyPropertyChanged
    {
        public Student()
        {
            StudentInitials = new Initials();
            this.RowIndex = -1;
            this.ColumnIndex = -1;
            TestAnswers = new List<Student_Answer>();
            Bonus_Points = 0;
            DatabaseID = -1;
            LoginKey = 1234;
            this.Status = Online_Status.Offline;
            this.WifiUsage = Wifi_Status.Abstaining;
        }

        //Variables
        public Online_Status Status
        {
            get { return _Status; }
            set
            {
                if (value != _Status)
                {
                    _Status = value;
                    OnPropertyChanged("Status");
                }
            }
        }
        public Wifi_Status WifiUsage
        {
            get { return _WifiUsage; }
            set
            {
                if (value != _WifiUsage)
                {
                    _WifiUsage = value;

                    //Update the WifiDetected Boolean
                    if (_WifiUsage == Wifi_Status.Using)
                        this.WifiDetected = true;

                    OnPropertyChanged("WifiUsage");
                }
            }
        }
        public Cheating_Role CheatingRole
        {
            get { return _CheatingRole; }
            set
            {
                if (value != _CheatingRole)
                {
                    _CheatingRole = value;
                    OnPropertyChanged("CheatingRole");
                }
            }
        }
        public string Name
        {
            get { return _Name; }
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    _Initials = StudentInitials.MakeInitials(_Name);
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Initials");
                }
            }
        }
        public string DefaultName { get { return "Student Name"; } }
        public string Initials { get { return _Initials; } }
        public double TestScore
        {
            get { return _TestScore; }
            set
            {
                if (value != _TestScore)
                {
                    _TestScore = value;
                    OnPropertyChanged("Test_Score");
                }
            }
        }
        public double TestProgress
        {
            get { return Math.Round(_TestProgress, 2); }
            set
            {
                if (value != _TestProgress)
                {
                    _TestProgress = value;
                    OnPropertyChanged("TestProgress");
                }
            }
        }
        public object DatabaseID { get; set; }
        public object ServerID { get; set; }
        public bool IsCheating
        {
            get { return _IsCheating; }
            set
            {
                if (value != _IsCheating)
                {
                    _IsCheating = value;
                    OnPropertyChanged("IsCheating");
                }
            }
        }
        public bool IsInClassroom
        {
            get { return _IsInClassroom; }
            set
            {
                if (value != _IsInClassroom)
                {
                    _IsInClassroom = value;
                    OnPropertyChanged("IsInClassroom");
                }
            }
        }
        public bool WifiDetected
        {
            get { return _WifiDetected; }
            set
            {
                if (value != _WifiDetected)
                {
                    _WifiDetected = value;
                    OnPropertyChanged("WifiDetected");
                }
            }
        }
        public double Bonus_Points { get; set; }
        public List<Student_Answer> TestAnswers { get; set; }
        private Initials StudentInitials;
        public int LoginKey
        {
            get { return _LoginKey; }
            set
            {
                if (value != _LoginKey)
                {
                    _LoginKey = value;
                    OnPropertyChanged("LoginKey");
                }
            }
        }
        public int RowIndex
        {
            get { return _RowIndex; }
            set
            {
                if (value != _RowIndex)
                {
                    _RowIndex = value;
                    OnPropertyChanged("RowIndex");
                }
            }
        }
        public int ColumnIndex
        {
            get { return _ColumnIndex; }
            set
            {
                if (value != _ColumnIndex)
                {
                    _ColumnIndex = value;
                    OnPropertyChanged("ColumnIndex");
                }
            }
        }

        //Methods

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

        //Private Variables
        private string _Name, _Initials;
        private double _TestScore, _TestProgress;
        private int _RowIndex, _ColumnIndex, _LoginKey;
        private bool _IsInClassroom, _WifiDetected, _IsCheating;
        private Online_Status _Status;
        private Wifi_Status _WifiUsage;
        private Cheating_Role _CheatingRole;
    }

    public class ReturnValidation
    {
        /// <summary>
        /// Returns true or false indicating whether the objective was executed.
        /// </summary>
        /// <param name="_IsOk">True or False</param>
        public ReturnValidation(bool _IsOk)
        {
            this.IsOk = _IsOk;
            this.Header = null;
            this.Body = null;
        }
        /// <summary>
        /// Returns true or false indicating whether the objective was executed while explaining the outcome.
        /// </summary>
        /// <param name="_IsOk">True or False</param>
        /// <param name="_Header">Header of the message.</param>
        /// <param name="_Body">Body of the message.</param>
        public ReturnValidation(bool _IsOk, string _Header, string _Body)
        {
            this.IsOk = _IsOk;
            this.Header = _Header;
            this.Body = _Body;
        }

        //Private Attributes
        private bool IsOk { get; set; }
        private string Header { get; set; }
        private string Body { get; set; }

        //Public Methods
        /// <summary>
        /// Returns if the objective was executed or not.
        /// </summary>
        /// <returns></returns>
        public bool GetIsOk() { return this.IsOk; }
        /// <summary>
        /// Gets the Header of the execution outcome.
        /// </summary>
        /// <returns>Returns null if empty.</returns>
        public string GetErrorHeader() { return this.Header; }
        /// <summary>
        /// Gets the Body of the execution outcome.
        /// </summary>
        /// <returns>Returns null if empty.</returns>
        public string GetErrorBody() { return this.Body; }
    }

    public class Class : INotifyPropertyChanged
    {
        public Class()
        {
            this.Students = new ObservableCollection<Student>();
            this.Tests = new ObservableCollection<Test>();
            this.ClassInitials = new Initials();
            this.NewStudent = new Student();
            this.NewTest = new Test();
            this.HasTests = false;
            this.Class_Name = this.DefaultName;
            this.DeleteStudent = new DeleteStudent_Command(this);
            this.AddStudent = new AddStudent_Command(this);
            this.PreAddTest = new PreAddTest_Command(this);
            this.AddTest = new AddTest_Command(this);
            this.DeleteTest = new DeleteTest_Command(this);
            this.ClassLayout = new ClassStructure(this);
            this.SelectedStudentNotNull = new SelectedStudentNotNull_Command(this);
        }

        //Variables
        public ObservableCollection<Student> Students
        {
            get { return _Students; }
            set
            {
                _Students = value;
                OnPropertyChanged("Students");
            }
        }
        public ObservableCollection<Test> Tests { get; set; }
        public string Class_Name
        {
            get { return _Class_Name; }
            set
            {
                _Class_Name = value;
                _Class_Intials = ClassInitials.MakeInitials(_Class_Name);
                OnPropertyChanged("Class_Name");
                OnPropertyChanged("Class_Initials");
            }
        }
        public string DefaultName { get { return "Class Name"; } }
        public int TestCount { get { return this.Tests.Count; } }
        public int StudentCount { get { return this.Students.Count; } }
        public string Class_Initials { get { return _Class_Intials; } }
        public Student NewStudent { get; set; }
        public Test NewTest { get; set; }
        public bool HasTests
        {
            get { return _HasTests; }
            set
            {
                if (value != _HasTests)
                {
                    _HasTests = value;
                    OnPropertyChanged("HasTests");
                }
            }
        }
        private Initials ClassInitials;
        public Student SelectedStudent
        {
            get { return _SelectedStudent; }
            set
            {
                _SelectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }
        public Test SelectedTest
        {
            get { return _SelectedTest; }
            set
            {
                _SelectedTest = value;
                OnPropertyChanged("SelectedTest");
            }
        }
        public ClassStructure ClassLayout { get; set; }

        //Methods
        public ReturnValidation Add_Student(Student student)
        {
            //Check if empty
            if (student.Name == null || student.Name == "")
                return new ReturnValidation(_IsOk: false, _Header: "Add Student", _Body: "A student must receive a valid name.");

            //Check if the student name matches any other student names
            foreach (Student tempStudent in this.Students)
                if (tempStudent.Name.ToLower() == student.Name.ToLower())
                    return new ReturnValidation(_IsOk: false, _Header: "Add Student", _Body: "A student by this name already exists, please choose another name.");

            //Add the student since no other student has the name
            this.Students.Add(new Student() { Name = student.Name });

            //Create a list from the observable collection of students so it can be sorted alphabetically
            List<Student> LocalStudents = new List<Student>(this.Students);
            LocalStudents.Sort((student1, student2) => string.Compare(student1.Name.Split(' ')[student1.Name.Split(' ').Length - 1], student2.Name.Split(' ')[student2.Name.Split(' ').Length - 1]));
            this.Students = new ObservableCollection<Student>(LocalStudents);

            //Update other lists that reference this list
            this.ClassLayout.RefreshDisjointStudents();

            //Return
            return new ReturnValidation(_IsOk: true);
        }
        public ReturnValidation Add_Test(Test test)
        {
            //Check if empty
            if (test.TestName == null || test.TestName == "")
                return new ReturnValidation(_IsOk: false, _Header: "Add Test", _Body: "A test must receive a valid name.");

            //Check if the test name is already taken
            foreach (Test temptest in this.Tests)
                if (temptest.TestName.ToLower() == test.TestName.ToLower())
                    return new ReturnValidation(_IsOk: false, _Header: "Add Test", _Body: "A test by this name already exists, please choose another name.");

            //Add the test since no other student has the name
            this.Tests.Add(test);
            this.HasTests = true;
            return new ReturnValidation(_IsOk: true);
        }
        public ReturnValidation ReName_Student(Student student, string NewName)
        {
            //Check if empty
            if (student.Name == null || student.Name == "")
                return new ReturnValidation(_IsOk: false, _Header: "Rename Student", _Body: "A student must receive a valid name.");

            //Check if the student name matches any other student names
            foreach (Student tempStudent in this.Students)
                if (NewName.ToLower() == tempStudent.Name.ToLower())
                    if (tempStudent != student)
                        return new ReturnValidation(_IsOk: false, _Header: "Rename Student", _Body: "A student by this name already exists.");

            student.Name = NewName;
            return new ReturnValidation(_IsOk: true);
        }
        public ReturnValidation ReName_Test(Test test, string NewName)
        {
            //Check if empty
            if (test.TestName == null || test.TestName == "")
                return new ReturnValidation(_IsOk: false, _Header: "Add Test", _Body: "A test must receive a valid name.");
            
            //Check if the test name matches any other tests
            foreach (Test tempTest in this.Tests)
                if (NewName.ToLower() == tempTest.TestName.ToLower())
                    if (tempTest != test)
                        return new ReturnValidation(_IsOk: false, _Header: "Rename Test", _Body: "A test by this name already exists.");

            test.TestName = NewName;
            return new ReturnValidation(_IsOk: true);
        }
        public void Delete_Student(Student stud)
        {
            this.Students.Remove(stud);
        }
        public void Delete_Test(Test test)
        {
            this.Tests.Remove(test);
            if (this.Tests.Count == 0)
                this.HasTests = false;
        }
        public bool IsStudentInList(Student student)
        {
            return this.Students.Contains(student);
        }
        public bool IsTestInList(Test test)
        {
            return this.Tests.Contains(test);
        }
        /// <summary>
        /// Changes the selected test to be the test at element zero if there is at least one test.
        /// </summary>
        public void ChangeSelectedTest()
        {
            if (this.HasTests)
                this.SelectedTest = this.Tests[0];
        }
        /// <summary>
        /// Changes the class' selected test.
        /// </summary>
        /// <param name="test">The test to change the selected test to.</param>
        public void ChangeSelectedTest(Test test)
        {
            if (this.IsTestInList(test))
                this.SelectedTest = test;
        }

        //Commands
        public DeleteStudent_Command DeleteStudent { get; set; }
        public AddStudent_Command AddStudent { get; set; }
        public PreAddTest_Command PreAddTest { get; set; }
        public AddTest_Command AddTest { get; set; }
        public DeleteTest_Command DeleteTest { get; set; }
        public SelectedStudentNotNull_Command SelectedStudentNotNull { get; set; }

        //Private Variables
        private string _Class_Intials, _Class_Name;
        private Test _SelectedTest;
        private Student _SelectedStudent;
        private ObservableCollection<Student> _Students;
        private bool _HasTests;

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
}
