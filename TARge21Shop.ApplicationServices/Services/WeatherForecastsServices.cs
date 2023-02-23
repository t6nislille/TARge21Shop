﻿using Nancy.Json;
using System.Net;
using TARge21Shop.Core.Dto.WeatherDtos;

namespace TARge21Shop.ApplicationServices.Services
{
    public class WeatherForecastsServices
    {
        public  async Task<WeatherResultDto> WeatherDetail(WeatherResultDto dto)
        {
            string apikey = "8eDECvxiN7UjAN31cXKlLeXLawl84Cn3";
            var url = $"http://dataservice.accuweather.com/forecasts/v1/daily/1day/";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                WeatherRootDto weatherInfo = (new JavaScriptSerializer()).Deserialize<WeatherRootDto>(json);

                weatherInfo.Headline.EffectiveDate = dto.EffectiveDate;
                weatherInfo.Headline.EffectiveEpochDate = dto.EffectiveEpochDate;
                weatherInfo.Headline.Severity = dto.Severity;
                weatherInfo.Headline.Text = dto.Text;
                weatherInfo.Headline.Category = dto.Category;
                weatherInfo.Headline.EndDate = dto.EndDate;
                weatherInfo.Headline.EndEpochDate = dto.EndEpochDate;

                weatherInfo.Headline.MobileLink = dto.MobileLink;
                weatherInfo.Headline.Link = dto.Link;

                weatherInfo.DailyForecasts[0].Date = dto.DailyForecastsDay;
                weatherInfo.DailyForecasts[0].EpochDate = dto.DailyForecastsEpochDate;

                weatherInfo.DailyForecasts[0].Temperature.Minimum.Value = dto.TempMinValue;
                weatherInfo.DailyForecasts[0].Temperature.Minimum.Unit = dto.TempMinUnit;
                weatherInfo.DailyForecasts[0].Temperature.Minimum.UnitType = dto.TempMinUnitType;

                weatherInfo.DailyForecasts[0].Temperature.Maximum.Value = dto.TempMaxValue;
                weatherInfo.DailyForecasts[0].Temperature.Maximum.Unit = dto.TempMaxUnit;
                weatherInfo.DailyForecasts[0].Temperature.Maximum.UnitType = dto.TempMaxUnitType;

                weatherInfo.DailyForecasts[0].Day.Icon = dto.DayIcon;
                weatherInfo.DailyForecasts[0].Day.IconPhrase = dto.DayIconPhrase;
                weatherInfo.DailyForecasts[0].Day.HasPrecipitation = dto.DayHasPrecipitation;
                weatherInfo.DailyForecasts[0].Day.PrecipitationType = dto.DayPrecipitationType;
                weatherInfo.DailyForecasts[0].Day.PrecipitationIntensity = dto.DayPrecipitationIntensity;

                weatherInfo.DailyForecasts[0].Night.Icon = dto.NightIcon;
                weatherInfo.DailyForecasts[0].Night.IconPhrase = dto.NightIconPhrase;
                weatherInfo.DailyForecasts[0].Night.HasPrecipitation = dto.NightHasPrecipitation;
                weatherInfo.DailyForecasts[0].Night.PrecipitationType = dto.NightPrecipitationType;
                weatherInfo.DailyForecasts[0].Night.PrecipitationIntensity = dto.NightPrecipitationIntensity;

            }

            return dto;
        }
    }
}
