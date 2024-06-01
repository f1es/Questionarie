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
using WpfQuestionnaire.MVVM.ViewModel.Pages;
using WpfQuestionnaire.MVVM.Views.Pages;
using WpfQuestionnaire.MVVM.Views.Windows;

namespace WpfQuestionnaire.MVVM.ViewModel.Windows
{
    public class ApplicationViewModel : ObservableObject
    {
        private Page _currentPage;
        private TeenQuestionPage _teenQuestionPage = new TeenQuestionPage(new TeenQuestionsPageViewModel());
        private ChildQuestionsPage _childQuestionsPage = new ChildQuestionsPage(new ChildQuestionsPageViewModel());
        private SwitchTestPage _switchTestPage = new SwitchTestPage(new SwitchTestViewModel());

        private static InactiveTimer _inactiveTimer = new InactiveTimer();
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }
        public TeenQuestionPage TeenQuestionPage 
        { 
            get => _teenQuestionPage; 
            private set => _teenQuestionPage = value; 
        }
        public ChildQuestionsPage ChildQuestionsPage 
        { 
            get => _childQuestionsPage; 
            private set => _childQuestionsPage = value; 
        }
        public SwitchTestPage SwitchTestPage
        {
            get => _switchTestPage;
            private set => _switchTestPage = value;
        }
        public static InactiveTimer InactiveTimer
        {
            get => _inactiveTimer;
            private set => _inactiveTimer = value;
        }

        public ICommand ClickCommand
        {
            get => new RelayCommand(c =>
            {
                //_inactiveTimer.ResetTimer();
            });
        }
        public ApplicationViewModel()
        {
            CurrentPage = SwitchTestPage;
        }
    }
}
