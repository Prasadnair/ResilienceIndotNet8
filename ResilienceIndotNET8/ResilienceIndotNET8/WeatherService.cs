using Polly.Registry;

namespace ResilienceIndotNET8
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider;
        public WeatherService(HttpClient httpClient,
                             ResiliencePipelineProvider<string> resiliencePipelineProvider)
        {
            _httpClient = httpClient;
            _resiliencePipelineProvider = resiliencePipelineProvider;
           
        }

        public async Task<string> GetWeatherAsync()
        {
            var pipeline = _resiliencePipelineProvider.GetPipeline("default");
            var response = await pipeline
                .ExecuteAsync( async ct=> await _httpClient.GetAsync($"https://localhost:7187/weatherforecast",ct));
           
            return await response.Content.ReadAsStringAsync();
        }

    }
}
