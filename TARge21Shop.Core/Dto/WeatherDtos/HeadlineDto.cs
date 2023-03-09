using System.Text.Json.Serialization;

namespace TARge21Shop.Core.Dto.WeatherDtos
{
    public class HeadlineDto
    {
        [JsonPropertyName("EffectiveDate")]
        public DateTime Date { get; set; }

        [JsonPropertyName("EffectiveEpochDate")]
        public int EpochDate { get; set; }

        [JsonPropertyName("Severity")]
        public int Severity { get; set; }

        [JsonPropertyName("Text")]
        public int Text { get; set; }

        [JsonPropertyName("Category")]
        public int Category { get; set; }

        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("EndEpochDate")]
        public int EndEpochDate { get; set; }

        [JsonPropertyName("MobileLink")]
        public string MobileLink { get; set; }

        [JsonPropertyName("Link")]
        public string Link { get; set; }

        public Temperature Temperature { get; set; }
        public Day Day { get; set; }
        public Night night { get; set; }
        public List<string> Sources { get; set; }
    }
}
