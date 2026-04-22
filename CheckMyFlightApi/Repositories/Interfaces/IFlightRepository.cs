using CheckMyFlightApi.Models;

namespace CheckMyFlightApi.Repositories.Interfaces;

public interface IFlightRepository
{
    Task<Flight?> GetFlightByFlightNumberAsync(string flightNumber,CancellationToken cancellationToken);
    Task<bool> FlightExistsAsync(string flightNumber,CancellationToken cancellationToken);
    Task<Flight> AddAsync(Flight flight,CancellationToken cancellationToken);
    Task<Flight> UpdateAsync(Flight flight,CancellationToken cancellationToken);
    Task<bool> DeleteByFlightNumberAsync(string flightNumber,CancellationToken cancellationToken);
}