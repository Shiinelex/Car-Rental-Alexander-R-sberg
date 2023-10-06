using Car_Rental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes;

public class Booking : IBooking
{

    public string RegNo { get; init; }
    public string Customer { get; init; }
    public int KmRented { get; set; }
    public int? KmReturned { get; set; }
    public DateTime DateRented { get; init; }
    public DateTime DateReturned { get; set; }
    public double? Cost { get; set; } 
    public double CostKM { get; set; }
    public double CostDay { get; set; }
    public bool IsOpen { get; set; }

    public Booking(IVehicle vehicle, IPerson person, int? kmReturned, DateTime dateRented, DateTime dateReturned, bool isOpen)
    {
        RegNo = vehicle.RegNo;
        Customer = $"{person.FirstName} {person.LastName} ({person.SSN})";
        KmRented = vehicle.Odometer;
        KmReturned = kmReturned;
        DateRented = dateRented;
        DateReturned = dateReturned;
        Cost = null;
        CostDay = vehicle.CostDay;
        IsOpen = isOpen;
    }

    public void CalculateCost()
    {
        Cost = ((DateReturned - DateRented).TotalDays * CostDay) 
             + ((KmReturned - KmRented) * CostKM);
    }
}
