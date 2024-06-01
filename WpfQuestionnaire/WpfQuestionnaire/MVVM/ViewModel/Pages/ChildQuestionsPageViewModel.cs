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
    public class ChildQuestionsPageViewModel : ObservableObject, ITest
    {
        private List<TwoButtonsQuestion> _questions = new List<TwoButtonsQuestion>();
        private List<Button> _answers = new List<Button>();
        private string _questionMessage;
        private string _navigationString;
        private string _backgroundSource;
        private double _progressBarValue;
        private int _currentQuestionIndex;

        private int _closeAppCounter;

        public List<Button> Answers
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

                if (value < 0)
                    return;

                if (value >= _questions.Count)
                {
                    FinishTest();
                    return;
                }

                _currentQuestionIndex = value;
                Answers = _questions[_currentQuestionIndex].ButtonAnswers;
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
                if (_questions[CurrentQuestionIndex].AnswerIndex != -1)
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
        public ICommand CloseApplication
        {
            get => new RelayCommand(c =>
            {
                if (_closeAppCounter < 50)
                    _closeAppCounter++;
                else
                    Application.Current.Shutdown();
            });
        }
        public ChildQuestionsPageViewModel()
        {
            _questions = FileLoader.LoadChildQuestions();

            foreach (var question in _questions)
            {
                foreach (var button in question.ButtonAnswers)
                {
                    var buttonAnswer = button.Content as TextBlock;

                    button.Command = new RelayCommand(c =>
                    {
                        question.AnswerIndex = question.Question.GetAnswersScore(buttonAnswer.Text);
                        UpdateProgressBar();
                        CurrentQuestionIndex += 1;
                    });
                }
            }

            CurrentQuestionIndex = 0;
        }
        public void UpdateNavigationString()
        {
            NavigationString = $"Вопрос {CurrentQuestionIndex + 1} из {_questions.Count}";
        }
        public int CalculateResultIndex()
        {
            List<int> answersFrequency = new List<int>();
            foreach(var question in _questions)
            {
                answersFrequency.Add(question.AnswerIndex);
            }

            var resultIndex = answersFrequency
                .GroupBy(i => i)
                .OrderByDescending(grp => grp.Count())
                .Select(i => i.Key)
                .First();

            return resultIndex;
        }
        public void MoveToExitPage()
        {
            var mainWindow = Application.Current.MainWindow.DataContext as ApplicationViewModel;
            ExitPageViewModel exitPageViewModel = new ExitPageViewModel(mainWindow.CurrentPage, BackgroundSource);
            mainWindow.CurrentPage = new ExitPage(exitPageViewModel);
        }
        public void Reset()
        {
            _closeAppCounter = 0;
            CurrentQuestionIndex = 0;
            foreach(var question in _questions)
            {
                question.AnswerIndex = -1;
            }
            UpdateProgressBar();
        }
        public void FinishTest()
        {
            int index = CalculateResultIndex();
            ChildResult result = FileLoader.LoadChildResults().FirstOrDefault(r => r.Index == index);
            ResultPageViewModel resultPageViewModel = new ResultPageViewModel(result);
            ResultPage resultPage = new ResultPage(resultPageViewModel);

            var mainWindow = Application.Current.MainWindow.DataContext as ApplicationViewModel;
            mainWindow.CurrentPage = resultPage;
            Reset();
        }
        public bool IsAllQuestionsAnswered()
        {
            foreach (var question in _questions)
            {
                if (question.AnswerIndex == -1)
                    return false;
            }

            return true;
        }
        public void UpdateProgressBar()
        {
            double questionsCount = _questions.Count;
            double answeredQuestions = _questions
                .Where(_ => _.AnswerIndex != -1)
                .Count();

            ProgressBarValue = answeredQuestions / questionsCount;
        }
    }
}
