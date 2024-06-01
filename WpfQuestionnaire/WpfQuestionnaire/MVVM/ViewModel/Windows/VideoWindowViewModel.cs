using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfQuestionnaire.MVVM.ViewModel.Windows
{
    public class VideoWindowViewModel : ObservableObject
    {
        public RelayCommand ClickCommand
        {
            get => new RelayCommand(c =>
            {
                WindowsHelper.CloseWindow(this);
            });
        }

        public string VideoPath
        {
            get => FileLoader.VideoPath;
        }
    }
}
