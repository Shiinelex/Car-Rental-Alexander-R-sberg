using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IVehicle
{
    public int Id { get; set; }
    public string RegNo { get; set; }
    public string Brand { get; set; }
    public double? Odometer { get; set; }
    public double? CostKM { get; set; }
    public VehicleType Type { get; set; }
    public double? CostDay { get; set; }
    public VehicleStatus Status { get; set; }

    public int? tempPerson { get; set; } 

}
