using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfQuestionnaire.MVVM.ViewModel.Windows
{
    public class ErrorWindowViewModel
    {
        public RelayCommand DoneCommand
        {
            get => new RelayCommand(c =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.DataContext == this)
                        window.Close();
                }
            });
        }
    }
}
