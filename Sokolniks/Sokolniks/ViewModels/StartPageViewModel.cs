using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Sokolniks.ViewModels
{
    class StartPageViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void DoPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private string _season;
        private string season
        {
            get { return _season; }
            set
            {
                _season = value;
                AnnouncementImage = "../Images/announcement-" + _season + ".png";
                MinimapImage = "../Images/minimap-" + _season + ".png";
            }
        }
        private DispatcherTimer seasonTimer = new DispatcherTimer();

        private string _AnnouncementImage;
        public string AnnouncementImage
        {
            get { return _AnnouncementImage; }
            set
            {
                _AnnouncementImage = value;
                DoPropertyChanged("AnnouncementImage");
            }
        }

        private string _MinimapImage;
        public string MinimapImage
        {
            get { return _MinimapImage; }
            set
            {
                _MinimapImage = value;
                DoPropertyChanged("MinimapImage");
            }
        }

        public StartPageViewModel()
        {
            GetSeason(DateTime.Now);
            AnnouncementImage = "../Images/announcement-" + season + ".png";
            MinimapImage = "../Images/minimap-" + season + ".png";
            var secLeft = 60 - DateTime.Now.Second;
            var minLeft = 60 - DateTime.Now.Minute - (secLeft > 0 ? 1 : 0);
            var hourLeft = 24 - DateTime.Now.Hour - (minLeft > 0 ? 1 : 0);
            seasonTimer.Interval = new TimeSpan(hourLeft, minLeft, secLeft);
            seasonTimer.Start();
            seasonTimer.Tick += SeasonTimer_Tick;

        }

        private void SeasonTimer_Tick(object sender, EventArgs e)
        {
            seasonTimer.Interval = new TimeSpan(24, 0, 0);
            GetSeason(DateTime.Now);
        }

        private void GetSeason(DateTime date)
        {
            if (date.Month >= 9 && date.Month <= 11)
                season = "autumn";
            else if ((date.Month == 4 && date.Day >= 15) || (date.Month >= 5 && date.Month <= 8))
                season = "summer";
            else
                season = "winter";
        }
    }
}
