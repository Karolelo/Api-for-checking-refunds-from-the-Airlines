using System.Text.Json.Serialization;

namespace CheckMyFlightApi.Models;

public class DepartureInfo
{
    [JsonPropertyName("airport")]
    public string? Airport { get; set; }
    
    [JsonPropertyName("iata")]
    public string? Iata { get; set; }
    
    [JsonPropertyName("scheduled")]
    public string? Scheduled { get; set; }
    
    [JsonPropertyName("estimated")]
    public string? Estimated { get; set; }
    
    [JsonPropertyName("actual")]
    public string? Actual { get; set; }
    
    [JsonPropertyName("delay")]
    public int? Delay { get; set; }
}