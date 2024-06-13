using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MauiWeather.MVVM.Models
{
    public class WeatherData
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public double elevation { get; set; }
        public Daily_units daily_units { get; set; }
        public Daily daily { get; set; }
    }

    public class Daily_units
    {
        public string time { get; set; }
        public string weather_code { get; set; }
        public string temperature_2m_max { get; set; }
        public string temperature_2m_min { get; set; }
    }

    public class Daily
    {
        public string[] time { get; set; }
        public int[] weather_code { get; set; }
        public double[] temperature_2m_max { get; set; }
        public double[] temperature_2m_min { get; set; }
    }


}