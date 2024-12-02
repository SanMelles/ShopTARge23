using Microsoft.AspNetCore.Mvc;
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
                return RedirectToAction("City", "OpenWeatherMaps", new { city = model.Name });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult City(string city)
        {
            OpenWeatherMapRootDto dto = new();
            dto.Name = city;

            _openWeatherMapServices.OpenWeatherMapResult(dto);

            OpenWeatherMapsViewModel vm = new OpenWeatherMapsViewModel
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






            return View("City", vm);
        }
    }
}