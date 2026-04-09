using System.Text.Json.Serialization;
using CheckMyFlightApi.Models;

namespace CheckMyFlightApi.DTOs;

public class AviationStackResponse
{
    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; set; }
    
    [JsonPropertyName("data")]
    public List<FlightData> Data { get; set; } = new();
}