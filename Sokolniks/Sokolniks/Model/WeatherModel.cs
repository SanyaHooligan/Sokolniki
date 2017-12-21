using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokolniks.Model
{
    public class Currently
    {
        
        public int time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public double precipIntensity { get; set; }
        public double precipProbability { get; set; }
        public string precipType { get; set; }
        public double temperature { get; set; }
        public double apparentTemperature { get; set; }
        public double dewPoint { get; set; }
        public double humidity { get; set; }
        public double pressure { get; set; }
        public double windSpeed { get; set; }
        public double windGust { get; set; }
        public int windBearing { get; set; }
        public int uvIndex { get; set; }
        public double ozone { get; set; }
    }


    public class Weather
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string timezone { get; set; }
        public Currently currently { get; set; }
        public int offset { get; set; }
    }
}
