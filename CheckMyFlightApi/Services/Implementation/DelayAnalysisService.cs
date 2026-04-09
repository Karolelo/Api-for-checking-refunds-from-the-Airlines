using System.Diagnostics;
using CheckMyFlightApi.Models;
using CheckMyFlightApi.Services.Interfaces;

namespace CheckMyFlightApi.Services.Implementation;
/// <summary>
/// If I had more time I would read about
/// return policies in EU, but I haven't soo I simply this class 
/// </summary>
public class DelayAnalysisService : IDelayAnalysisService
{
    public string GetDelayStatus(FlightData data)
    {
        if (data == null)
            return "No information about flight";
        
        if (data.FlightStatus?.ToLower() == "cancelled")
        {
            return "Flight cancelled";
        }
        
        var departureDelay = data.Departure?.Delay ?? 0;
        var arrivalDelay = data.Arrival?.Delay ?? 0;
        
        var maxDelay = Math.Max(departureDelay, arrivalDelay);

        var result = $"{GetDelayStatusByMinutes(maxDelay)} you can get about {GetApproximatedAmountOfReturn(maxDelay)} USD";

        return result;
    }

    public bool IsFlightDelayed(FlightData data)
    {
        var departureDelay = data.Departure?.Delay ?? 0;
        var arrivalDelay = data.Arrival?.Delay ?? 0;

        var maxDelay = Math.Max(departureDelay, arrivalDelay);

        return maxDelay > 15;
    }

    public string GetDelayStatusByMinutes(int minutes)
    {
        return minutes switch
        {
            <= 0 => "No delay",
            <= 15 => "Minor delay",
            <= 30 => "Small delay",
            <= 60 => "Medium delay",
            <= 120 => "Big delay"
        };
    }

    /// <summary>
    /// Function which going to give us
    /// approximated money we can get back
    /// for a delayed flight
    /// </summary>
    /// <returns></returns>
    public int GetApproximatedAmountOfReturn(int minutes)
    {
        return minutes switch
        {
            <= 0 => 0,
            <= 15 => 0,
            <= 30 => 200,
            <= 60 => 500,
            <= 120 => 800
        };
    }
}