using System.Text.Json.Serialization;

namespace CheckMyFlightApi.Models;

public class AirlineInfo
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("iata")]
    public string? Iata { get; set; }
}