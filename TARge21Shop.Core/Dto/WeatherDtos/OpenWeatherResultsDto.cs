using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARge21Shop.Core.Dto.WeatherDtos
{
    public class OpenWeatherResultsDto
    {
        public string Name { get; set; }
        public double Temperature { get; set; }
        public double Feels_Like { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double Speed { get; set; }
        public string Description { get; set; }
    }
}
