using CheckMyFlightApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CheckMyFlightApi.Services.Interfaces;

public interface IFlightService
{
    Task<ActionResult<FlightApiResponse>> GetInformationAboutFlight(string flightNumber,CancellationToken cancellationToken);
}