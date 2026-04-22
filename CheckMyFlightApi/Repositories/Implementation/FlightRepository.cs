using CheckMyFlightApi.Data.DbContext;
using CheckMyFlightApi.Models;
using CheckMyFlightApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CheckMyFlightApi.Repositories.Implementation;

public class FlightRepository : IFlightRepository
{
    private readonly MyDbContext _context;

    public FlightRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<Flight?> GetFlightByFlightNumberAsync(string flightNumber,CancellationToken cancellationToken)
    {
        return await _context.Flights
            .FirstOrDefaultAsync(f => f.FlightNumber == flightNumber,cancellationToken);
    }
    
    public async Task<bool> FlightExistsAsync(string flightNumber,CancellationToken cancellationToken)
    {
        return await _context.Flights
            .AnyAsync(f => f.FlightNumber == flightNumber,cancellationToken);
    }
    
    public async Task<Flight> AddAsync(Flight flight,CancellationToken cancellationToken)
    {
        var result = await _context.Flights.AddAsync(flight,cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
    
    public async Task<Flight> UpdateAsync(Flight flight,CancellationToken cancellationToken)
    {
        _context.Flights.Update(flight);
        
        await _context.SaveChangesAsync(cancellationToken);
        return flight;
    }

    public async Task<bool> DeleteByFlightNumberAsync(string flightNumber,CancellationToken cancellationToken)
    {
        var flight = await GetFlightByFlightNumberAsync(flightNumber,cancellationToken);
        if (flight == null)
            return false;

        _context.Flights.Remove(flight);
        
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}