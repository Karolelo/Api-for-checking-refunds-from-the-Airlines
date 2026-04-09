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

    public async Task<Flight?> GetFlightByFlightNumberAsync(string flightNumber)
    {
        return await _context.Flights
            .FirstOrDefaultAsync(f => f.FlightNumber == flightNumber);
    }
    
    public async Task<bool> FlightExistsAsync(string flightNumber)
    {
        return await _context.Flights
            .AnyAsync(f => f.FlightNumber == flightNumber);
    }
    
    public async Task<Flight> AddAsync(Flight flight)
    {
        var result = await _context.Flights.AddAsync(flight);
        
        await _context.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<Flight> UpdateAsync(Flight flight)
    {
        _context.Flights.Update(flight);
        
        await _context.SaveChangesAsync();
        return flight;
    }

    public async Task<bool> DeleteByFlightNumberAsync(string flightNumber)
    {
        var flight = await GetFlightByFlightNumberAsync(flightNumber);
        if (flight == null)
            return false;

        _context.Flights.Remove(flight);
        
        await _context.SaveChangesAsync();
        return true;
    }
}