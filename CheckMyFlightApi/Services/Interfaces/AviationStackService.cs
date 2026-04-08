using System.Text.Json;
using CheckMyFlightApi.Models;
using CheckMyFlightApi.Services.Implementation;

namespace CheckMyFlightApi.Services.Interfaces;

public class AviationStackService : IAviationStackService
{
    private readonly HttpClient _httpClient;
    private IConfiguration _configuration;
    
    public async Task<AviationStackResponse> GetFlightByFlightNumber(string flightNumber)
    {
        var url = $"{_configuration.GetSection("avion_api_url")}/flights" +
                  $"?access_key={_configuration.GetSection("avion_api_key")}" +
                  $"&flight_iata={flightNumber}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<AviationStackResponse>(content);

        return result ?? new AviationStackResponse();
    }
}