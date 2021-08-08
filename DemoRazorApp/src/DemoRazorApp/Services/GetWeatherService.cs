using DemoRazorApp.Model;
using Microsoft.Extensions.Configuration;
using Flurl;

using System.Threading.Tasks;
using Flurl.Http;
using System.Text.Json;

namespace DemoRazorApp.Services
{
    public class GetWeatherService : IGetWeatherInfoService
    {
        private readonly string baseUri;
        private readonly string apiKey;

        public GetWeatherService(IConfiguration configuration)
        {
            var config = configuration.GetSection("WeatherAPI");
            baseUri = config["BaseUri"];
            apiKey = config["ApiKey"];
        }

        public async Task<WeatherServiceResponse> GetWeatherForLocationAsync(string location)
        {
            var response = await baseUri
                .AppendPathSegment("/v1/current.json")
                .SetQueryParams(new { key = apiKey, q = location })
                .GetAsync();

            var content = await response.ResponseMessage.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<WeatherServiceResponse>(content, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return result;
        }
    }
}
