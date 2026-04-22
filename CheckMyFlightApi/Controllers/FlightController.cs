using CheckMyFlightApi.DTOs;
using CheckMyFlightApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CheckMyFlightApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
    private readonly IFlightService _flightService;
    
    public FlightController(IFlightService flightService)
    {
        _flightService = flightService;
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<FlightApiResponse>> GetFlightInfo(string flightNumber,CancellationToken cancellationToken)
    {
        try
        {
            var response = await _flightService.GetInformationAboutFlight(flightNumber, cancellationToken);
            return response;
        }catch(OperationCanceledException e)
        {
            return StatusCode(StatusCodes.Status499ClientClosedRequest);
        }
    }
}