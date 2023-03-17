﻿using Microsoft.AspNetCore.Mvc;
using TARge21Shop.Core.Dto.WeatherDtos;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Models.Weather;

namespace TARge21Shop.Controllers
{
    public class WeatherForecastsController : Controller
    {
        public readonly IWeatherForecastServices _weatherForecastServices;

        public WeatherForecastsController
        (
            IWeatherForecastServices weatherForecastServices
        )
        {
            _weatherForecastServices = weatherForecastServices;
        }

        public IActionResult Index()
        {
            WeatherViewModel vm = new WeatherViewModel();

            return View(vm);
        }

        [HttpPost]
        public IActionResult ShowWeather()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "WeatherForecasts");
            }
            return View();
        }

        [HttpGet]
        public IActionResult City()
        {
            WeatherResultDto dto = new();

            _weatherForecastServices.WeatherDetail(dto);

            WeatherViewModel vm = new();

            vm.Date = dto.EffectiveDate;
            vm.EpochDate = dto.EffectiveEpochDate;
            vm.Severity = dto.Severity;
            vm.Text = dto.Text;
            vm.MobileLink = dto.MobileLink;
            vm.Link = dto.Link;
            vm.Category = dto.Category;

            vm.TempMinValue = dto.TempMinValue;
            vm.TempMinUnit = dto.TempMinUnit;
            vm.TempMinUnitType = dto.TempMinUnitType;

            vm.TempMaxValue = dto.TempMaxValue;
            vm.TempMaxUnit = dto.TempMaxUnit;
            vm.TempMaxUnitType = dto.TempMaxUnitType;

            vm.DayIcon = dto.DayIcon;
            vm.DayIconPhrase = dto.DayIconPhrase;
            vm.DayHasPercipitation = dto.DayHasPrecipitation;
            vm.DayPrecipitationType = dto.DayPrecipitationType;
            vm.DayPrecipitationIntensity = dto.DayPrecipitationIntensity;

            vm.NightIcon = dto.NightIcon;
            vm.NightIconPhrase = dto.NightIconPhrase;
            vm.NightHasPrecipitation = dto.NightHasPrecipitation;
            vm.NightPrecipitationType = dto.NightPrecipitationType;
            vm.NightPrecipitationIntensity = dto.NightPrecipitationIntensity;

            return View(vm);
        }

        [HttpPost]
        public IActionResult ShowOpenWeather()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("OpenWeatherCity", "WeatherForecasts");
            }
            return View();
        }

        [HttpGet]
        public IActionResult OpenWeatherCity()
        {
            OpenWeatherResultsDto dto = new();

            _weatherForecastServices.OpenWeatherDetail(dto);

            OpenWeatherViewModel vm = new();

            vm.Name = dto.Name;
            vm.Temperature = dto.Temperature;
            vm.Feels_Like = dto.Feels_Like;
            vm.Humidity = dto.Humidity;
            vm.Pressure = dto.Pressure;
            vm.Speed = dto.Speed;
            vm.Description = dto.Description;
    
            return View(vm);
        }
    }
}
