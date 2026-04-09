using CheckMyFlightApi.Models;

namespace CheckMyFlightApi.Services.Interfaces;

public interface IDelayAnalysisService
{
    /// <summary>
    /// Gonna implemented here different status for returning
    /// money to passenger and save it into Information
    /// </summary>
    /// <returns></returns>
    string GetDelayStatus(FlightData data);
    bool IsFlightDelayed(FlightData data);
}