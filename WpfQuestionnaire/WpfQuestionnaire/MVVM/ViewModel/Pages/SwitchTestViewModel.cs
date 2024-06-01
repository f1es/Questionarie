using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WpfQuestionnaire.MVVM.ViewModel.Windows;
using WpfQuestionnaire.MVVM.Views.Windows;

namespace WpfQuestionnaire.MVVM.ViewModel.Pages
{
    public class SwitchTestViewModel : ObservableObject
    {
        public ICommand StartTeenTestCommand
        {
            get => new RelayCommand(c =>
            {
                StartTeenTest();
            });
        }

        public ICommand StartChildTestCommand
        {
            get => new RelayCommand(c =>
            {
                StartChildTest();
            });
        }
        
        public ICommand OpenVideo
        {
            get => new RelayCommand(c =>
            {
                if (!WindowsHelper.IsWindowExists(typeof(VideoWindow)))
                {
                    var mainWindowContext = Application.Current.MainWindow.DataContext as ApplicationViewModel;

                    if (mainWindowContext.CurrentPage != mainWindowContext.SwitchTestPage)
                        mainWindowContext.CurrentPage = mainWindowContext.SwitchTestPage;

                    new VideoWindow().Show();
                }
            });
        }

        public ICommand ClickCommand
        {
            get => new RelayCommand(c =>
            {

            });
        }
        public void StartChildTest()
        {
            var mainWindow = Application.Current.MainWindow.DataContext as ApplicationViewModel;

            var childTest = mainWindow.ChildQuestionsPage.DataContext as ChildQuestionsPageViewModel;
            childTest.Reset();

            mainWindow.CurrentPage = mainWindow.ChildQuestionsPage;
        }
        public void StartTeenTest()
        {
            var mainWindow = Application.Current.MainWindow.DataContext as ApplicationViewModel;

            var teenTest = mainWindow.TeenQuestionPage.DataContext as TeenQuestionsPageViewModel;
            teenTest.Reset();

            mainWindow.CurrentPage = mainWindow.TeenQuestionPage;
        }
    }
}
