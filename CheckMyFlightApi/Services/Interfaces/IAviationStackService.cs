using CheckMyFlightApi.DTOs;

namespace CheckMyFlightApi.Services.Interfaces;

public interface IAviationStackService
{
    /// <summary>
    /// We're getting flight-by-flight number
    /// WARNING (in flight radar its IATA, I'm not sure why ?)
    /// </summary>
    /// <param name="flightNumber"></param>
    /// <returns></returns>
    Task<AviationStackResponse> GetFlightByFlightNumber(string flightNumber,CancellationToken cancellationToken);
}