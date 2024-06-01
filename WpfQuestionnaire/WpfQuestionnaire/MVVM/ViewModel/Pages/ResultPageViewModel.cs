using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfQuestionnaire.MVVM.Models.Interfaces;
using WpfQuestionnaire.MVVM.ViewModel.Windows;

namespace WpfQuestionnaire.MVVM.ViewModel.Pages
{
    public class ResultPageViewModel : ObservableObject
    {
        private string _resultMessage;
        private string _header;
        private string _backgroundSource;
        public string ResultMessage
        {
            get => _resultMessage;
            set
            {
                _resultMessage = value;
                OnPropertyChanged();
            }
        }
        public string Header
        {
            get => _header;
            set
            {
                _header = value;
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
        public ResultPageViewModel(IResult result)
        {
            ResultMessage = result.ResultMessage;
            Header = result.Header;
            BackgroundSource = result.BackgroundSource;
        }
        public ICommand DoneCommand
        {
            get => new RelayCommand(c =>
            {
                var mainWindow = Application.Current.MainWindow.DataContext as ApplicationViewModel;
                mainWindow.CurrentPage = mainWindow.SwitchTestPage;
            });
        }
    }
}
