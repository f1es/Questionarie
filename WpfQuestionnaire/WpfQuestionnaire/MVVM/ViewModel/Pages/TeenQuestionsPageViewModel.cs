using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfQuestionnaire.MVVM.Models;
using WpfQuestionnaire.MVVM.ViewModel.Interfaces;
using WpfQuestionnaire.MVVM.ViewModel.Windows;
using WpfQuestionnaire.MVVM.Views.Pages;

namespace WpfQuestionnaire.MVVM.ViewModel.Pages
{
    public class TeenQuestionsPageViewModel : ObservableObject, ITest
    {
        private List<RadioButtonQuestion> _questions = new List<RadioButtonQuestion>();
        private List<RadioButton> _answers;
        private string _questionMessage;
        private string _navigationString;
        private string _backgroundSource;
        private double _progressBarValue;
        private int _currentQuestionIndex;
        public List<RadioButton> Answers
        {
            get => _answers;
            set
            {
                _answers = value;
                OnPropertyChanged();
            }
        }
        public string QuestionMessage
        {
            get => _questionMessage;
            set
            {
                _questionMessage = value;
                OnPropertyChanged();   
            }
        }
        public string NavigationString 
        { 
            get => _navigationString; 
            set
            {
                _navigationString = value;
                OnPropertyChanged();
            }
        }
        public string BackgroundSource
        {
            get => _backgroundSource;
            set
            {
                _backgroundSource = value;
                OnPropertyChanged();
            }
        }
        public double ProgressBarValue
        {
            get => _progressBarValue;
            set
            {
                _progressBarValue = value;
                OnPropertyChanged();
            }
        }
        public int CurrentQuestionIndex 
        { 
            get => _currentQuestionIndex;
            set
            {
                if (_questions.Count == 0)
                    return;

                if (value < 0 || value >= _questions.Count)
                    return;

                
                _currentQuestionIndex = value;
                Answers = _questions[_currentQuestionIndex].Answers;
                QuestionMessage = _questions[_currentQuestionIndex].QuestionMessage;
                BackgroundSource = _questions[_currentQuestionIndex].Question.BackgroundSource;
                UpdateNavigationString();
                

                OnPropertyChanged();
            }
        }
        public ICommand NextQuestionCommand
        {
            get => new RelayCommand(c =>
            {
                if (_questions[CurrentQuestionIndex].GetCheckedAnswerIndex() != -1)
                    CurrentQuestionIndex += 1;
            });
        }
        public ICommand PreviousQuestionCommand
        {
            get => new RelayCommand(c =>
            {
                CurrentQuestionIndex -= 1;
            });
        }
        public ICommand ContinueCommand
        {
            get => new RelayCommand(c =>
            {
                if (IsAllQuestionsAnswered())
                {
                    FinishTest();
                }
                else
                {
                    NextQuestionCommand.Execute(null);
                }
            });
        }
        public ICommand ExitCommand
        {
            get => new RelayCommand(c =>
            {
                MoveToExitPage();
            });
        }
        public TeenQuestionsPageViewModel()
        {
            _questions = FileLoader.LoadTeenQuestions(); 

            foreach(var question in _questions)
            {
                foreach (var answer in question.Answers)
                {
                    answer.Command = new RelayCommand(c => 
                    {
                        UpdateProgressBar();
                    });
                }
            }

            CurrentQuestionIndex = 0;
        }
        public void UpdateNavigationString()
        {
            NavigationString = $"Вопрос {CurrentQuestionIndex + 1} из {_questions.Count}";
        }
        public int CalculateScore()
        {
            int score = 0;
            foreach (var question in _questions)
            {
                score += question.GetScore();
            }
            return score;
        }
        public void FinishTest()
        {
            int score = CalculateScore();
            TeenResult result = FileLoader
                .LoadTeenResults()
                .FirstOrDefault(r => r.MinScore <= score && r.MaxScore >= score);

            ResultPageViewModel resultPageViewModel = new ResultPageViewModel(result);
            ResultPage resultPage = new ResultPage(resultPageViewModel);

            var mainWindow = Application.Current.MainWindow.DataContext as ApplicationViewModel;
            mainWindow.CurrentPage = resultPage;
            Reset();
        }
        public void MoveToExitPage()
        {
            var mainWindow = Application.Current.MainWindow.DataContext as ApplicationViewModel;
            ExitPageViewModel exitPageViewModel = new ExitPageViewModel(mainWindow.CurrentPage, BackgroundSource);
            mainWindow.CurrentPage = new ExitPage(exitPageViewModel);
        }
        public void Reset()
        {
            foreach (var question in _questions)
            {
                question.ResetQuestion();
            }
            CurrentQuestionIndex = 0;
            UpdateProgressBar();
        }
        public bool IsAllQuestionsAnswered()
        {
            foreach (var question in _questions)
            {
                if (question.GetScore() == -1)
                    return false;
            }

            return true;
        }
        public void UpdateProgressBar()
        {
            double questionsCount = _questions.Count;
            double answeredQuestions = _questions
                .Where(_ => _.GetScore() != -1)
                .Count();

            ProgressBarValue = answeredQuestions / questionsCount;
        }
    }
}
