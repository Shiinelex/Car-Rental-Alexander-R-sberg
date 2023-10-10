using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Vehicle: IVehicle
{
    public int Id { get; init; }
    public int bookingId { get; set; }
    public string RegNo { get; set; }
    public string Brand { get; init; }
    public double? Odometer { get; set; }
    public VehicleType Type { get; init; }
    public double? CostKM { get; set; }
    public double? CostDay { get; set; }
    public VehicleStatus Status { get; set; }
}
