using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Classes;

public class Motorcycle : Vehicle
{

    public Motorcycle(int id, string regNo, string brand, double? odometer, double? costKM, double? costDay, VehicleStatus vehicleStatus = default)
    {
        Id = id;
        RegNo = regNo;
        Brand = brand;
        Odometer = odometer;
        Type = VehicleType.Motorcycle;
        CostKM = costKM;
        CostDay = costDay;
        Status = vehicleStatus;
    }
}
