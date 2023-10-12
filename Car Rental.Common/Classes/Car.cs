using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Classes;

public class Car : Vehicle
{

    public Car(int id, string regNo, string brand, double? odometer, double? costKM, VehicleType type, double? costDay, VehicleStatus vehicleStatus = default)
    {
        Id = id;
        RegNo = regNo;
        Brand = brand;
        Odometer = odometer;
        CostKM = costKM;
        Type = type;
        CostDay = costDay;
        Status = vehicleStatus;
        tempPerson = null;
    }
}
