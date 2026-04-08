using System.Text.Json.Serialization;

namespace CheckMyFlightApi.Models;

public class AviationStackResponse
{
    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; set; }
    
    [JsonPropertyName("data")]
    public List<FlightData> Data { get; set; } = new();
}