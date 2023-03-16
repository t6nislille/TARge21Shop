using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TARge21Shop.Core.Dto.WeatherDtos
{
    public class OpenWeatherDto
    {
        [JsonPropertyName("weather")]
        public List<Weathers> Weather { get; set; }

        [JsonPropertyName("main")]
        public Mains Main { get; set; }

        [JsonPropertyName("wind")]
        public Winds Wind { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Weathers
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

    }

    public class Mains
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class Winds
    {
        [JsonPropertyName("wind_speed")]
        public double WindSpeed { get; set; }

    }
}
