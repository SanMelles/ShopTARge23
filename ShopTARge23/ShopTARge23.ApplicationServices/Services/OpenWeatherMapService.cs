using Nancy.Json;
using ShopTARge23.Core.Dto.WeatherDtos.OpenWeatherMapDtos;
using ShopTARge23.Core.ServiceInterface;
using System.Net;

namespace ShopTARge23.ApplicationServices.Services
{
    public class OpenWeatherMapServices : IOpenWeatherMapServices
    {
        public async Task<OpenWeatherMapRootDto> OpenWeatherMapResult(OpenWeatherMapRootDto dto)
        {
            string owmApiKey = "3c30044ddadc64c76f097cad4aa90a41";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={dto.Name}&appid={owmApiKey}&units=metric";
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                OpenWeatherMapRootDto openWeatherMapResult = new JavaScriptSerializer().Deserialize<OpenWeatherMapRootDto>(json);
                // Map all properties
                dto.Id = openWeatherMapResult.Id;
                dto.Name = openWeatherMapResult.Name;
                dto.Timezone = openWeatherMapResult.Timezone;
                dto.Dt = openWeatherMapResult.Dt;
                dto.Cod = openWeatherMapResult.Cod;
                dto.Clouds = openWeatherMapResult.Clouds;
                dto.Coord = openWeatherMapResult.Coord;
                dto.Main = openWeatherMapResult.Main;
                dto.Visibility = openWeatherMapResult.Visibility;
                dto.Wind = openWeatherMapResult.Wind;
                dto.Weather = openWeatherMapResult.Weather;
                dto.Sys = openWeatherMapResult.Sys;
            }
            return dto;
        }
    }
}