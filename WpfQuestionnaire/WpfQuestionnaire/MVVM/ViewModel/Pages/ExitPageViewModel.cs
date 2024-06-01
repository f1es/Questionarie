using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfQuestionnaire.MVVM.ViewModel.Interfaces;
using WpfQuestionnaire.MVVM.ViewModel.Windows;

namespace WpfQuestionnaire.MVVM.ViewModel.Pages
{
    public class ExitPageViewModel : ObservableObject
    {
        private string _backgroundSource;
        private Page _previousPage;

        public string BackgroundSource
        {
            get => _backgroundSource;
            set
            {
                _backgroundSource = value;
                OnPropertyChanged();
            }
        }

        public ExitPageViewModel(Page currentPage, string backgroundSource)
        {
            BackgroundSource = backgroundSource;
            _previousPage = currentPage;
        }

        public ICommand DoneCommand
        {
            get => new RelayCommand(c =>
            {
                if (_previousPage is ITest)
                {
                    ((ITest)_previousPage).Reset();
                }

                var mainWindow = Application.Current.MainWindow.DataContext as ApplicationViewModel;
                mainWindow.CurrentPage = mainWindow.SwitchTestPage;
            });
        }

        public ICommand CancelCommand
        {
            get => new RelayCommand(c =>
            {
                var mainWindow = Application.Current.MainWindow.DataContext as ApplicationViewModel;
                mainWindow.CurrentPage = _previousPage;
            });
        }
    }
}
