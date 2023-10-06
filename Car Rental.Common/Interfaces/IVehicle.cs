using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IVehicle
{
    public string RegNo { get; set; }
    public string Brand { get; init; }
    public int Odometer { get; set; }
    public float CostKM { get; set; }
    public VehicleType Type { get; init; }
    public int CostDay { get; set; }
    public VehicleStatus VehicleStatus { get; set; }

}
