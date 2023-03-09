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

        public IActionResult index()
        {
            WeatherWievModel vm = new WeatherWievModel();

            return View(vm);
        }

        [HttpGet]
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
            WeatherWievModel vm = new();

            _weatherForecastServices.WeatherDetail(dto);

            vm.Date = dto.EffectiveDate;
            vm.EpochDate = dto.EffectiveEpochDate;
            vm.Severity = dto.Severity;
            vm.Text = dto.Text;
            vm.MobileLink = dto.MobileLink;
            vm.Link = dto.Link;
            vm.Category = dto.Category;

            vm.Temperature.Minimum.Value = dto.TempMinValue;
            vm.Temperature.Minimum.Unit = dto.TempMinUnit;
            vm.Temperature.Minimum.UnitType = dto.TempMinUnitType;

            vm.Temperature.Maximum.Value = dto.TempMaxValue;
            vm.Temperature.Maximum.Unit = dto.TempMaxUnit;
            vm.Temperature.Maximum.UnitType = dto.TempMaxUnitType;

            vm.DayCycle.Icon = dto.DayIcon;
            vm.DayCycle.IconPhrase = dto.DayIconPhrase;
            vm.DayCycle.HasPrecipitation = dto.DayHasPrecipitation;
            vm.DayCycle.PrecipitationType = dto.DayPrecipitationType;
            vm.DayCycle.PrecipitationIntensity = dto.DayPrecipitationIntensity;

            vm.NightCycle.Icon = dto.NightIcon;
            vm.NightCycle.IconPhrase = dto.NightIconPhrase;
            vm.NightCycle.HasPrecipitation = dto.NightHasPrecipitation;
            vm.NightCycle.PrecipitationType = dto.NightPrecipitationType;
            vm.NightCycle.PrecipitationIntensity = dto.NightPrecipitationIntensity;

            return View(vm);
        }
    }
}
