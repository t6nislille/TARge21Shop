using Nancy.Json;
using System.Net;
using TARge21Shop.Core.Dto.WeatherDtos;
using TARge21Shop.Core.ServiceInterface;

namespace TARge21Shop.ApplicationServices.Services
{
    public class WeatherForecastsServices : IWeatherForecastServices
    {
        public async Task<WeatherResultDto> WeatherDetail(WeatherResultDto dto)
        {
            string apikey = "8eDECvxiN7UjAN31cXKlLeXLawl84Cn3";
            var url = $"http://dataservice.accuweather.com/forecasts/v1/daily/1day/127964?apikey=8eDECvxiN7UjAN31cXKlLeXLawl84Cn3&metric=true";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                WeatherRootDto weatherInfo = new JavaScriptSerializer().Deserialize<WeatherRootDto>(json);

                dto.EffectiveDate = weatherInfo.Headline.EffectiveDate;
                dto.EffectiveEpochDate = weatherInfo.Headline.EffectiveEpochDate;
                dto.Severity = weatherInfo.Headline.Severity;
                dto.Text = weatherInfo.Headline.Text;
                dto.Category = weatherInfo.Headline.Category;
                dto.EndDate = weatherInfo.Headline.EndDate;
                dto.EndEpochDate = weatherInfo.Headline.EndEpochDate;

                dto.MobileLink = weatherInfo.Headline.MobileLink;
                dto.Link = weatherInfo.Headline.Link;

                dto.TempMinValue = weatherInfo.DailyForecasts[0].Temperature.Minimum.Value;
                dto.TempMinUnit = weatherInfo.DailyForecasts[0].Temperature.Minimum.Unit;
                dto.TempMinUnitType = weatherInfo.DailyForecasts[0].Temperature.Minimum.UnitType;

                dto.TempMaxValue = weatherInfo.DailyForecasts[0].Temperature.Maximum.Value;
                dto.TempMaxUnit = weatherInfo.DailyForecasts[0].Temperature.Maximum.Unit;
                dto.TempMaxUnitType = weatherInfo.DailyForecasts[0].Temperature.Maximum.UnitType;

                dto.DayIcon = weatherInfo.DailyForecasts[0].Day.Icon;
                dto.DayIconPhrase = weatherInfo.DailyForecasts[0].Day.IconPhrase;
                dto.DayHasPrecipitation = weatherInfo.DailyForecasts[0].Day.HasPrecipitation;
                dto.DayPrecipitationType = weatherInfo.DailyForecasts[0].Day.PrecipitationType;
                dto.DayPrecipitationIntensity = weatherInfo.DailyForecasts[0].Day.PrecipitationIntensity;

                dto.NightIcon = weatherInfo.DailyForecasts[0].Night.Icon;
                dto.NightIconPhrase = weatherInfo.DailyForecasts[0].Night.IconPhrase;
                dto.NightHasPrecipitation = weatherInfo.DailyForecasts[0].Night.HasPrecipitation;
                dto.NightPrecipitationType = weatherInfo.DailyForecasts[0].Night.PrecipitationType;
                dto.NightPrecipitationIntensity = weatherInfo.DailyForecasts[0].Night.PrecipitationIntensity;

            }

            return dto;
        }

        public async Task<OpenWeatherResultsDto> OpenWeatherDetail(OpenWeatherResultsDto dto)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q=Liiva&appid=7c159c1147da6aa6503e89af99458a7d";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                var result = new JavaScriptSerializer().Deserialize<OpenWeatherDto>(json);

                dto.Name = result.Name;
                dto.Temperature = Math.Round(result.Main.Temp - 272.15, 2);
                dto.Feels_Like = Math.Round(result.Main.Feels_Like - 272.15, 2);
                dto.Humidity = result.Main.Humidity;
                dto.Pressure = result.Main.Pressure;
                dto.Speed = result.Wind.Speed;
                dto.Description = result.Weather[0].Description;
            }
            return dto;
        }
    }
}
