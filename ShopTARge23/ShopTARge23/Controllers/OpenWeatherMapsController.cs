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

        public IActionResult Index(string? city)
        {
            ViewData["Title"] = "OpenWeatherMap";

            // Initialize the ViewModel
            OpenWeatherMapsIndexViewModel vm = new();

            if (!string.IsNullOrWhiteSpace(city))
            {
                OpenWeatherMapRootDto dto = new() { Name = city };

                _openWeatherMapServices.OpenWeatherMapResult(dto);

                // Validate the API response
                if (dto != null && dto.Main != null && dto.Weather != null && dto.Weather.Any())
                {
                    // Map the DTO to the ViewModel
                    vm = new OpenWeatherMapsIndexViewModel
                    {
                        Name = dto.Name,
                        Temp = dto.Main.Temp,
                        FeelsLike = dto.Main.FeelsLike,
                        Pressure = dto.Main.Pressure,
                        Humidity = dto.Main.Humidity,
                        WindSpeed = dto.Wind.Speed,
                        WeatherConditions = dto.Weather.Select(w => new WeatherCondition
                        {
                            Main = w.Main,
                            Description = w.Description,
                        }).ToList(),
                    };
                }
                else
                {
                    TempData["Error"] = "No data found for the specified city. Please check the name and try again.";
                }
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult SearchCity(OpenWeatherMapsSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Pass the city name to the Index action
                return RedirectToAction("Index", new { city = model.Name });
            }

            TempData["Error"] = "Invalid input. Please try again.";
            return RedirectToAction("Index");
        }
    }
}