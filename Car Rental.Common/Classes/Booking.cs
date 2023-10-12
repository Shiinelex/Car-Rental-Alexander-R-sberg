using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System;

namespace Car_Rental.Common.Classes;

public class Booking : IBooking
{
    public int Id { get; init; }
    public string RegNo { get; init; }
    public string Customer { get; init; }
    public double? KmRented { get; set; }
    public double? KmReturned { get; set; } = null;
    public DateTime DateRented { get; init; }
    public DateTime DateReturned { get; set; }
    public double? Cost { get; set; } 
    public double? CostKM { get; set; }
    public double? CostDay { get; set; }
    public BookingStatus Status { get; set; } = default;

    public Booking(int id, IVehicle vehicle, IPerson person)
    {
        Id = id;
        RegNo = vehicle.RegNo;
        Customer = $"{person.FirstName} {person.LastName} ({person.SSN})";
        KmRented = vehicle.Odometer;
        DateRented = DateTime.Now;
        Cost = null;
        CostDay = vehicle.CostDay;
        CostKM = vehicle.CostKM;
        Status = BookingStatus.Open;
    }
}
