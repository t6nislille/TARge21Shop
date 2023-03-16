namespace TARge21Shop.Models.Weather
{
    public class OpenWeatherViewModel
    {
        public string Name { get; set; }
        public double Temperature { get; set; }
        public double TempFeelsLike { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double WindSpeed { get; set; }
        public string Description { get; set; }
    }
}
