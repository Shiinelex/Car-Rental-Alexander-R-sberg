using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Motorcycle : IVehicle
{
    public string RegNo { get; set; }
    public string Brand { get; init; }
    public int Odometer { get; set; }
    public float CostKM { get; set; }
    public VehicleType Type { get; init; }
    public int CostDay { get; set; }
    public VehicleStatus VehicleStatus { get; set; }

    public Motorcycle(string regNo, string brand, int odometer, float costKM, VehicleType type, int costDay, VehicleStatus vehicleStatus = default)
    {
        this.RegNo = regNo;
        this.Brand = brand;
        this.Odometer = odometer;
        this.CostKM = costKM;
        this.Type = type;
        this.CostDay = costDay;
        this.VehicleStatus = vehicleStatus;
    }

    public void GetVehicle()
    {

    }
}
