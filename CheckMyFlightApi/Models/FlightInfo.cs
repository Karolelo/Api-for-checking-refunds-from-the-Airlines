using System.Text.Json.Serialization;

namespace CheckMyFlightApi.Models;

public class FlightInfo
{
    [JsonPropertyName("number")]
    public string? Number { get; set; }
    
    [JsonPropertyName("iata")]
    public string? Iata { get; set; }
}