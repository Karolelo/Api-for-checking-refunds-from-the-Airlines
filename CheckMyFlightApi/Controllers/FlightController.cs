using System.Text.RegularExpressions;
using CheckMyFlightApi.DTOs;
using CheckMyFlightApi.Services.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace CheckMyFlightApi.Controllers;

[Route(("api/[controller]"))]
[ApiController]
public class FlightController : ControllerBase
{
    private readonly IAviationStackService _aviationStackService;
    
    public FlightController(IAviationStackService aviationStackService)
    {
        _aviationStackService = aviationStackService;
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<AviationStackResponse>> GetFlightInfo(string flightNumber)
    {
        if (!Regex.IsMatch(flightNumber, $"[A-Z\\d]{{4,6}}"))
            return BadRequest("The flight number does not match");

        var response = await _aviationStackService.GetFlightByFlightNumber(flightNumber);

        if (response.Pagination.Count > 0)
            return new ActionResult<AviationStackResponse>(response);

        return NotFound("Sorry but we not found your flight");
    }
}