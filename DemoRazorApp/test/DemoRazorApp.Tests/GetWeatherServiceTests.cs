using Microsoft.Extensions.Configuration;
using DemoRazorApp.Services;
using Moq;
using Flurl.Http.Testing;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using FluentAssertions.Execution;

namespace DemoRazorApp.Tests.Services
{

    public class GetWeatherServiceTests
    {
        [Fact]
        public async Task EndpointGetsMappedCorrectly()
        {
            // Arrange
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(_ => _[It.Is<string>(s => s == "ApiKey")]).Returns("testAPIKey");
            mockConfSection.SetupGet(_ => _[It.Is<string>(s => s == "BaseUri")]).Returns("http://api.weatherapi.com");

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(_ => _.GetSection(It.Is<string>(s => s == "WeatherAPI"))).Returns(mockConfSection.Object);

            var httpTest = new HttpTest();
            httpTest.RespondWith("{\"location\":{},\"current\":{}}");
            var weatherService = new GetWeatherService(mockConfiguration.Object);

            // Act
            var sut = await weatherService.GetWeatherForLocationAsync("london");

            // Assert
            var expectedEndpoint = "http://api.weatherapi.com/v1/current.json?key=testAPIKey&q=london";
            httpTest.ShouldHaveCalled(expectedEndpoint);
        }

        [Fact]
        public async Task SuccessfulResponseMapsToWeatherServiceResponse()
        {
            // Arrange
            var response = 
                "{" +
                    "\"location\":{" +
                        "\"name\":\"London\"," +
                        "\"region\":\"CityofLondon,GreaterLondon\"," +
                        "\"country\":\"UnitedKingdom\"," +
                        "\"lat\":51.52," +
                        "\"lon\":-0.11," +
                        "\"tz_id\":\"Europe/London\"," +
                        "\"localtime_epoch\":1628372020," +
                        "\"localtime\":\"2021-08-0722:33\"}," +
                    "\"current\":{" +
                        "\"last_updated_epoch\":1628371800," +
                        "\"last_updated\":\"2021-08-0722:30\"," +
                        "\"temp_c\":17.0," +
                        "\"temp_f\":62.6," +
                        "\"is_day\":0," +
                        "\"condition\":{" +
                            "\"text\":\"Partlycloudy\"," +
                            "\"icon\":\"//cdn.weatherapi.com/weather/64x64/night/116.png\"," +
                            "\"code\":1003" +
                        "}," +
                        "\"wind_mph\":15.0," +
                        "\"wind_kph\":24.1," +
                        "\"wind_degree\":240," +
                        "\"wind_dir\":\"WSW\"," +
                        "\"pressure_mb\":1000.0," +
                        "\"pressure_in\":29.53," +
                        "\"precip_mm\":1.4," +
                        "\"precip_in\":0.06," +
                        "\"humidity\":72," +
                        "\"cloud\":50," +
                        "\"feelslike_c\":17.0," +
                        "\"feelslike_f\":62.6," +
                        "\"vis_km\":10.0," +
                        "\"vis_miles\":6.0," +
                        "\"uv\":4.0," +
                        "\"gust_mph\":17.4," +
                        "\"gust_kph\":28.1" +
                    "}" +
                "}";

            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(_ => _[It.Is<string>(s => s == "ApiKey")]).Returns("testAPIKey");
            mockConfSection.SetupGet(_ => _[It.Is<string>(s => s == "BaseUri")]).Returns("http://api.weatherapi.com");

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(_ => _.GetSection(It.Is<string>(s => s == "WeatherAPI"))).Returns(mockConfSection.Object);

            var httpTest = new HttpTest();
            httpTest.RespondWith(response);

            var weatherService = new GetWeatherService(mockConfiguration.Object);

            // Act
            var result = await weatherService.GetWeatherForLocationAsync("london");

            // Assert
            using (new AssertionScope())
            {
                result.Location.Name.Should().Be("London");
                result.Location.Region.Should().Be("CityofLondon,GreaterLondon");
                result.Location.Country.Should().Be("UnitedKingdom");
                result.Location.Lat.Should().Be(51.52F);
                result.Location.Lon.Should().Be(-0.11F);
                result.Location.TzId.Should().Be("Europe/London");
                result.Location.LocalTime.Should().Be("2021-08-0722:33");
                result.Location.LocalTimeEpoch.Should().Be(1628372020);

                result.Current.Condition.Text.Should().Be("Partlycloudy");
                result.Current.Condition.Icon.Should().Be("//cdn.weatherapi.com/weather/64x64/night/116.png");
                result.Current.Condition.Code.Should().Be(1003);

                result.Current.LastUpdated.Should().Be("2021-08-0722:30");
                result.Current.LastUpdatedEpoch.Should().Be(1628371800);
                result.Current.TempC.Should().Be(17.0F);
                result.Current.TempF.Should().Be(62.6F);
                result.Current.IsDay.Should().Be(0);
                result.Current.WindMph.Should().Be(15.0F);
                result.Current.WindKph.Should().Be(24.1F);
                result.Current.WindDegree.Should().Be(240);
                result.Current.WindDir.Should().Be("WSW");
                result.Current.PressureMb.Should().Be(1000.0F);
                result.Current.PressureIn.Should().Be(29.53F);
                result.Current.PrecipMm.Should().Be(1.4F);
                result.Current.PrecipIn.Should().Be(0.06F);
                result.Current.Humidity.Should().Be(72);
                result.Current.Cloud.Should().Be(50);
                result.Current.FeelsLikeC.Should().Be(17.0F);
                result.Current.FeelsLikeF.Should().Be(62.6F);
                result.Current.VisKm.Should().Be(10.0F);
                result.Current.VisMiles.Should().Be(6.0F);
                result.Current.Uv.Should().Be(4.0F);
                result.Current.GustMph.Should().Be(17.4F);
                result.Current.GustKph.Should().Be(28.1F);
            }
        }
    }
}
