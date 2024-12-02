﻿using Microsoft.AspNetCore.Mvc;
using ShopTARge23.Core.Dto.WeatherDtos.OpenWeatherMapDtos;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Models.OpenWeatherMaps;

namespace ShopTARge23.Controllers
{
    public class OpenWeatherMapsController : Controller
    {
        private readonly IOpenWeatherMapServices _openWeatherMapServices;
        public OpenWeatherMapsController
            (
                IOpenWeatherMapServices openWeatherMapServices
            )
        {
            _openWeatherMapServices = openWeatherMapServices;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "OpenWeatherMap";
            return View();
        }
        [HttpPost]
        public IActionResult SearchCity(OpenWeatherMapsSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "OpenWeatherMaps", new { city = model.CityName });
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult City(string city)
        {
            OpenWeatherMapRootDto dto = new();
            dto.CityName = city;
            _openWeatherMapServices.OpenWeatherMapResult(dto);
            OpenWeatherMapsViewModel vm = new();
            vm.CityName = dto.CityName;
            vm.Temp = dto.Main.Temp;
            vm.FeelsLike = dto.Main.FeelsLike;
            vm.Humidity = dto.Main.Humidity;
            vm.Pressure = dto.Main.Pressure;
            vm.Speed = dto.Wind.Speed;
            return View("City", vm);
        }
    }
}