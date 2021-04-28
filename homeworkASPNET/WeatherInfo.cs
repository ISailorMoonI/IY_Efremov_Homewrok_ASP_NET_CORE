using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeworkASPNET
{
    public class WeatherInfo
    {
        public int Temperature { get; set; }
        public DateTime Date { get; set; }

        private List<WeatherInfo> info = new List<WeatherInfo>();
    }
}
