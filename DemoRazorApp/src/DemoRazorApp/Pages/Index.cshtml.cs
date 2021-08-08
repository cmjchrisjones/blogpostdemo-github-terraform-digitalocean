using DemoRazorApp.Mappers;
using DemoRazorApp.Model;
using DemoRazorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoRazorApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IGetWeatherInfoService _getWeatherService;

        public IndexModel(IGetWeatherInfoService getWeatherInfoService, ILogger<IndexModel> logger)
        {
            _getWeatherService = getWeatherInfoService;
            _logger = logger;
        } 

        public WeatherServiceResponse WeatherServiceResponse { get; set; }

        [BindProperty]
        public string Location { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                ModelState.AddModelError("Location Not Specified", "Please specify a location to get the weather forecast");
                return Page();
            }

            await Task.CompletedTask;

            try
            {
                WeatherServiceResponse = await _getWeatherService.GetWeatherForLocationAsync(location);
                if (WeatherServiceResponse != null)
                {
                    return Page();
                }
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("It broken", $"We screwed up somehow, our pc tells us this was the error message: {ex.Message}");
                return Page();
            }
        }
    }
}
