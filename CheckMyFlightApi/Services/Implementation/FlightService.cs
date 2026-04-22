using CheckMyFlightApi.DTOs;
using CheckMyFlightApi.Models;
using CheckMyFlightApi.Repositories.Interfaces;
using CheckMyFlightApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace CheckMyFlightApi.Services.Implementation;

public class FlightService : IFlightService
{
    private IDelayAnalysisService _delayAnalysisService;
    private IAviationStackService _aviationStackService;
    private IFlightRepository _flightRepository;

    public FlightService(IDelayAnalysisService delayAnalysisService, IAviationStackService aviationStackService, IFlightRepository flightRepository)
    {
        _delayAnalysisService = delayAnalysisService;
        _aviationStackService = aviationStackService;
        _flightRepository = flightRepository;
    }

     public async Task<ActionResult<FlightApiResponse>> GetInformationAboutFlight(string flightNumber,CancellationToken cancellationToken)
    {
        // Check if the flight is already finished and in the database
        if (await _flightRepository.FlightExistsAsync(flightNumber,cancellationToken))
        {
            var existingFlight = await _flightRepository.GetFlightByFlightNumberAsync(flightNumber,cancellationToken);
            
            if (existingFlight != null)
            {
                return new FlightApiResponse
                {
                    Response = $"Flight {flightNumber} has ended. Information: {existingFlight.Information}. "
                };
            }
        }
        
        // Flight not in a database, fetch from Aviation Stack API
        var aviationStackResponse = await _aviationStackService.GetFlightByFlightNumber(flightNumber,cancellationToken);
        
        if (aviationStackResponse?.Data == null || !aviationStackResponse.Data.Any())
        {
            return new FlightApiResponse
            {
                Response = $"No information found for flight {flightNumber}"
            };
        }
        
        var flightData = aviationStackResponse.Data.First();
        var delayStatus = _delayAnalysisService.GetDelayStatus(flightData);
       
        
        // Check if flight has ended (landed, cancelled, diverted)
        var flightStatus = flightData.FlightStatus?.ToLower();
        var isFlightEnded = flightStatus == "landed" || 
                           flightStatus == "cancelled" || 
                           flightStatus == "diverted";
        
        // Save to te database if flight has ended
        if (isFlightEnded)
        {
            var flight = new Flight
            {
                FlightNumber = flightNumber,
                ArrivalPlace = flightData.Arrival.Airport,
                DeparturePlace = flightData.Departure.Airport,
                Information = delayStatus
            };
            
            await _flightRepository.AddAsync(flight,cancellationToken);
            
            return new FlightApiResponse
            {
                Response = $"Flight {flightNumber} has ended and been saved to database. " +
                          $"Status: {flightData.FlightStatus}. {delayStatus}"
            };
        }

        var num = new HashSet<int>();
        // Flight is still active
        return new FlightApiResponse
        {
            Response = $"Flight {flightNumber} is currently {flightData.FlightStatus}. {delayStatus}"
        };
    }
}