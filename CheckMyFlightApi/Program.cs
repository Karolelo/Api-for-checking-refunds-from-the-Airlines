using CheckMyFlightApi.Data.DbContext;
using CheckMyFlightApi.Infrastructure;
using CheckMyFlightApi.Repositories.Implementation;
using CheckMyFlightApi.Repositories.Interfaces;
using CheckMyFlightApi.Services.Implementation;
using CheckMyFlightApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CheckMyFlightApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        //Comment if u already have database
        /*var seeder = new DatabaseSeeder();
        seeder.SeedDatabase();*/
        
        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IAviationStackService, AviationStackService>();
        builder.Services.AddScoped<IDelayAnalysisService,DelayAnalysisService>();
        builder.Services.AddScoped<IFlightService, FlightService>(); 
        builder.Services.AddScoped<IFlightRepository, FlightRepository>();
        builder.Services.AddHttpClient<AviationStackService>();
        
        // Configuration of DbContext
        var dbPath = Path.Combine(AppContext.BaseDirectory, "database.db");
        builder.Services.AddDbContext<MyDbContext>(c => c.UseSqlite($"Data Source={dbPath}"));
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flight application");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}