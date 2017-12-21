using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokolniks.Pages;
using HtmlAgilityPack;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Net;
using Newtonsoft.Json.Linq;
using Sokolniks.Model;
using System.Windows.Input;
using System.Windows;

namespace Sokolniks.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void DoPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Properties
        private Dictionary<int, string> MonthsByKey = new Dictionary<int, string>()
        {
            {1, "января" }, {2, "февраля" }, {3, "марта" }, {4, "апреля" },
            {5, "мая" }, {6, "июня" }, {7, "июля" }, {8, "августа" },
            {9, "сентября" }, {10, "октября" }, {11, "ноября" }, {12, "декабря" },
        };
        private Dictionary<string, string> WeatherInRussian = new Dictionary<string, string>()
        {
            { "cloudy", "облачно" }, { "clear-day", "ясно" }, { "clear-night", "ясно" }, { "rain", "дождь" },
            { "snow", "снег" }, { "sleet", "джодь\r\nсо снегом" }, { "windy", "ветрено" }, { "fog", "туман" },
            { "partly-cloudy-day", "переменная\r\nоблачность" }, { "partly-cloudy-night", "переменная\r\nоблачность" },
        };
        private DispatcherTimer MinuteTimer = new DispatcherTimer();
        private DispatcherTimer WeatherTimer = new DispatcherTimer();
        private Weather CurrentWeather = new Weather();

        private string _WorkingHours;
        public string WorkingHours
        {
            get { return _WorkingHours; }
            set
            {
                _WorkingHours = value;
                DoPropertyChanged("Source");
            }
        }

        private string _PhoneNumber;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set
            {
                _PhoneNumber = value;
                DoPropertyChanged("PhoneNumber");
            }
        }

        private string _Source;
        public string Source
        {
            get { return _Source; }
            set
            {
                _Source = value;
                DoPropertyChanged("Source");
            }
        }

        private String _TimeString;
        public String TimeString
        {
            get { return _TimeString; }
            set
            {
                _TimeString = value;
                DoPropertyChanged("TimeString");
            }
        }

        private String _DateString;
        public String DateString
        {
            get { return _DateString; }
            set
            {
                _DateString = value;
                DoPropertyChanged("DateString");
            }
        }

        private String _TemperatureString;
        public String TemperatureString
        {
            get { return _TemperatureString; }
            set
            {
                _TemperatureString = value;
                DoPropertyChanged("TemperatureString");
            }
        }

        private String _WeatherString;
        public String WeatherString
        {
            get { return _WeatherString; }
            set
            {
                if (WeatherInRussian.ContainsKey(value))
                    _WeatherString = WeatherInRussian[value];
                else
                _WeatherString = value;
                DoPropertyChanged("WeatherString");
            }
        }
        #endregion


        public MainWindowViewModel()
        {
            Source = "../Pages/StartPage.xaml";
            WorkingHours = "9:00-20:00 ежедневно";
            PhoneNumber = "+7 499 39-39-222";
            LoadTime();
            LoadDate();
            try
            {
                LoadWeather();
            }
            catch(WebException ex)
            {
                MessageBox.Show("Нет подключения к интернету");
            }
            WeatherTimer.Interval = new TimeSpan(0, 10, 0);
            WeatherTimer.Tick += WeatherTimer_Tick;
            WeatherTimer.Start();
        }

        private void LoadTime()
        {
            var timeNow = DateTime.Now;
            TimeString = $"{timeNow.Hour.ToString("D2")}:{timeNow.Minute.ToString("D2")}";
            MinuteTimer.Interval = new TimeSpan(0, 0, 60 - timeNow.Second);
            MinuteTimer.Tick += MinuteTimer_Tick;
            MinuteTimer.Start();
        }

        private void MinuteTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                LoadDate();
            MinuteTimer.Interval = new TimeSpan(0, 0, 60);
            TimeString = $"{DateTime.Now.Hour.ToString("D2")}:{DateTime.Now.Minute.ToString("D2")}";
        }

        private void LoadDate()
        {
            var timeNow = DateTime.Now;
            DateString = $"{timeNow.Day} {MonthsByKey[timeNow.Month]}";
        }

        private void LoadWeather()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://park.sokolniki.com/api/weather/");
                CurrentWeather = Newtonsoft.Json.JsonConvert.DeserializeObject<Weather>(json);
            }
            WeatherString = CurrentWeather.currently.icon;
            string sign = CurrentWeather.currently.temperature > 0 ? "+" : string.Empty;
            TemperatureString = sign + $"{(int)CurrentWeather.currently.temperature + 1}" + (char)0xb0;
        }

        private void WeatherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                LoadWeather();
            }
            catch(WebException ex)
            {
                MessageBox.Show("Нет подключения к интернету");
            }
        }
    }
}
