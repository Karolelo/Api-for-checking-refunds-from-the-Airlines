using CheckMyFlightApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckMyFlightApi.Data.DbContext;

public class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Flight> Flights { get; set; }
    
}