using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WpfQuestionnaire.MVVM.ViewModel.Windows;
using WpfQuestionnaire.MVVM.Views.Windows;

namespace WpfQuestionnaire.MVVM.ViewModel
{
    public class InactiveTimer
    {
        private DispatcherTimer videoTimer = new DispatcherTimer();

        public DispatcherTimer VideoTimer
        {
            get => videoTimer;
            private set => videoTimer = value;
        }
        public InactiveTimer()
        {
            videoTimer.Tick += new EventHandler(TimeOut);
            videoTimer.Interval = new TimeSpan(0, 10, 0);
            videoTimer.Start();
        }

        public void TimeOut(object sender, EventArgs e)
        {
            if (!WindowsHelper.IsWindowExists(typeof(VideoWindow)))
            {
                var mainWindowContext = Application.Current.MainWindow.DataContext as ApplicationViewModel;

                if (mainWindowContext.CurrentPage != mainWindowContext.SwitchTestPage)
                    mainWindowContext.CurrentPage = mainWindowContext.SwitchTestPage;

                new VideoWindow().Show();
            }
        }

        public void ResetTimer()
        {
            videoTimer.Stop();
            videoTimer.Start();
        }
    }
}
