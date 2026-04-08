using System.Text.Json.Serialization;

namespace CheckMyFlightApi.Models;

public class FlightData
{
    [JsonPropertyName("flight_date")]
    public string? FlightDate { get; set; }
    
    [JsonPropertyName("flight_status")]
    public string? FlightStatus { get; set; }
    
    [JsonPropertyName("departure")]
    public DepartureInfo? Departure { get; set; }
    
    [JsonPropertyName("arrival")]
    public ArrivalInfo? Arrival { get; set; }
    
    [JsonPropertyName("airline")]
    public AirlineInfo? Airline { get; set; }
    
    [JsonPropertyName("flight")]
    public FlightInfo? Flight { get; set; }
}