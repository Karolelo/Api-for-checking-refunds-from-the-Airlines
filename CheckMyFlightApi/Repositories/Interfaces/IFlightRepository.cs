using CheckMyFlightApi.Models;

namespace CheckMyFlightApi.Repositories.Interfaces;

public interface IFlightRepository
{
    Task<Flight?> GetFlightByFlightNumberAsync(string flightNumber);
    Task<bool> FlightExistsAsync(string flightNumber);
    Task<Flight> AddAsync(Flight flight);
    Task<Flight> UpdateAsync(Flight flight);
    Task<bool> DeleteByFlightNumberAsync(string flightNumber);
}