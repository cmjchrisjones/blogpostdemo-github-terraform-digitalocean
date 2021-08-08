using DemoRazorApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoRazorApp.Services
{
    public interface IGetWeatherInfoService
    {
        Task<WeatherServiceResponse> GetWeatherForLocationAsync(string location);
    }
}
