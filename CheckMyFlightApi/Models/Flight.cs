namespace CheckMyFlightApi.Models;

public class Flight
{
    public int Id { get; set; } 
    public string FlightNumber { get; set; }
    public string DeparturePlace { get; set; }
    public string ArrivalPlace { get; set; }
    public string CanGetReturnMoney { get; set; }
}