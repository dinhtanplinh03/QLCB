using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace QLCB.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "4a47957a9e8d1d17d7bd964299da8ac4";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetWeatherDescriptionAsync(string city)
        {
            try
            {
                string url = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}&units=metric&lang=vi";

                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return $"❌ Không lấy được thời tiết: {response.ReasonPhrase}";

                var content = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(content).RootElement;

                // Lấy phần mô tả thời tiết của bản ghi đầu tiên (gần nhất)
                var description = json
                    .GetProperty("list")[0]
                    .GetProperty("weather")[0]
                    .GetProperty("description")
                    .GetString();

                return description ?? "Không rõ";
            }
            catch (Exception ex)
            {
                return $"❌ Lỗi khi lấy thời tiết: {ex.Message}";
            }
        }
    }
}
