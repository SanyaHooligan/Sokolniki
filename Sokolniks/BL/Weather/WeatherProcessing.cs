using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BL.Weather
{
    public class WeatherProcessing
    {
        public WeatherProcessing()
        {
            LoadWeather();
            //WeatherTimer.Interval = new TimeSpan(0, 10, 0);
            //WeatherTimer.Tick += WeatherTimer_Tick;
            //WeatherTimer.Start();
        }
        private Dictionary<int, string> MonthsByKey = new Dictionary<int, string>()
        {
            {1, "января" }, {2, "февраля" }, {3, "марта" }, {4, "апреля" },
            {5, "мая" }, {6, "июня" }, {7, "июля" }, {8, "августа" },
            {9, "сентября" }, {10, "октября" }, {11, "ноября" }, {12, "декабря" },
        };
        private Dictionary<string, string> WeatherInRussian = new Dictionary<string, string>()
        {
            { "cloudy", "облачно" }, { "clear-day", "ясно" }, { "clear-night", "ясно" }, { "rain", "дождь" },
            { "snow", "снег" }, { "sleet", "джодь\r\nсо снегом" }, { "windy", "ветрено" }, { "wind", "ветер" }, { "fog", "туман" },
            { "partly-cloudy-day", "переменная\r\nоблачность" }, { "partly-cloudy-night", "переменная\r\nоблачность" },
        };

        //private DispatcherTimer WeatherTimer = new DispatcherTimer();
        private Weather CurrentWeather = new Weather();

        private String _WeatherState;
        public String WeatherState
        {
            get { return _WeatherState; }
            set
            {
                if (WeatherInRussian.ContainsKey(value))
                    _WeatherState = WeatherInRussian[value];
                else
                    _WeatherState = value;
            }
        }

        public String TemperatureValue { get; set; }

        public void LoadWeather()
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    var json = wc.DownloadString("http://park.sokolniki.com/api/weather/");
                    CurrentWeather = Newtonsoft.Json.JsonConvert.DeserializeObject<Weather>(json);
                }
                catch{ return; }
            }
            WeatherState = CurrentWeather.currently.icon;
            string sign = CurrentWeather.currently.temperature > 0 ? "+" : string.Empty;
            TemperatureValue = sign + $"{(int)CurrentWeather.currently.temperature + 1}" + (char)0xb0;
        }

        //private void WeatherTimer_Tick(object sender, EventArgs e)
        //{
        //    LoadWeather();
        //}
    }
}
