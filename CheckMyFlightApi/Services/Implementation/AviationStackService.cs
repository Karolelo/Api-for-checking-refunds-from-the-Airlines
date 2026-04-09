using System.Text.Json;
using CheckMyFlightApi.DTOs;
using CheckMyFlightApi.Services.Interfaces;

namespace CheckMyFlightApi.Services.Implementation;

public class AviationStackService : IAviationStackService
{
    private readonly HttpClient _httpClient;
    private IConfiguration _configuration;

    public AviationStackService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<AviationStackResponse> GetFlightByFlightNumber(string flightNumber)
    {
        var url = $"{_configuration["avion_api_url"]}flights" + 
                  $"?access_key={_configuration["avion_api_key"]}" + 
                  $"&flight_iata={flightNumber}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<AviationStackResponse>(content);
        
        Console.WriteLine(result);
        return result ?? new AviationStackResponse();
    }
}