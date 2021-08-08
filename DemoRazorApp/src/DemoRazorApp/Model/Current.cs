using System.Text.Json;
using System.Text.Json.Serialization;

namespace DemoRazorApp.Model
{
    public class Current
    {
        [JsonPropertyName("last_updated_epoch")]
        public int LastUpdatedEpoch { get; set; }

        [JsonPropertyName("last_updated")]
        public string LastUpdated { get; set; }

        [JsonPropertyName("temp_c")]
        public float TempC { get; set; }

        [JsonPropertyName("temp_f")]
        public float TempF { get; set; }

        public int IsDay { get; set; }

        public Condition Condition { get; set; }

        [JsonPropertyName("wind_mph")]

        public float WindMph { get; set; }

        [JsonPropertyName("wind_kph")]
        public float WindKph { get; set; }

        [JsonPropertyName("wind_degree")]
        public int WindDegree { get; set; }

        [JsonPropertyName("wind_dir")]
        public string WindDir { get; set; }

        [JsonPropertyName("pressure_mb")]
        public float PressureMb { get; set; }

        [JsonPropertyName("pressure_in")]
        public float PressureIn { get; set; }

        [JsonPropertyName("precip_mm")]
        public float PrecipMm { get; set; }

        [JsonPropertyName("precip_in")]
        public float PrecipIn { get; set; }

        public int Humidity { get; set; }

        public int Cloud { get; set; }

        [JsonPropertyName("feelslike_c")]
        public float FeelsLikeC { get; set; }

        [JsonPropertyName("feelslike_f")]
        public float FeelsLikeF { get; set; }

        [JsonPropertyName("vis_km")]
        public float VisKm { get; set; }

        [JsonPropertyName("vis_miles")]
        public float VisMiles { get; set; }

        public float Uv { get; set; }

        [JsonPropertyName("gust_mph")]
        public float GustMph { get; set; }

        [JsonPropertyName("gust_kph")]
        public float GustKph { get; set; }
    }

}