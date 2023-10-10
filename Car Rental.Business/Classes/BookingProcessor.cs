using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;
using System.Linq;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    private readonly IData _db;

    public BookingProcessor(IData db) => _db = db;

    #region Variables
    //Booking
    public double? personId;
    public double? KmReturned;
    public double? Cost;

    //Vehicle
    public string regNo = string.Empty;
    public string brand = string.Empty;
    public double? odometer;
    public double? costKM;
    public double? costDay;
    public VehicleType type;

    public double? distance;
    //Person
    public int? sSN;
    public string? firstName = string.Empty;
    public string? lastName = string.Empty;
    #endregion

    #region Get Methods
    public IEnumerable<IPerson> GetPersons()
    {
        IEnumerable<IPerson> p = _db.GetPersons();
        return p;
    }
    public IEnumerable<IVehicle> GetVehicles(VehicleStatus status = default)
    {
        IEnumerable<IVehicle> v = _db.GetVehicles(status);
        return v;
    }
    public IEnumerable<IBooking> GetBookings()
    {
        IEnumerable<IBooking> b = _db.GetBooking();

        foreach (var booking in b)
        {
            if (booking.Status == BookingStatus.Open) { continue; }
            //booking.CalculateCost();
        }
        return b;
    }
    #endregion

    public void AddPerson()
    {
        if (!CheckCustomerInputs(sSN, lastName, firstName)) { return; }
        _db.Add(new Customer(_db.NextPersonId, sSN, firstName, lastName));
        ResetCustomerInputs();
        
    }
    public void AddVehicle()
    {
        if (!CheckVehicleInputs(regNo, brand, odometer, costKM, costDay)) { return; }
        if (type == VehicleType.Motorcycle)
        {
            _db.Add(new Motorcycle(_db.NextVehicleId, regNo, brand, odometer, costKM, costDay));
            ResetVehicleInputs();
        }
        else
        {
            _db.Add(new Car(_db.NextVehicleId, regNo, brand, odometer, costKM, type, costDay));
            ResetVehicleInputs();
        }
    }
    public void RentVehicle(IVehicle vehicle)
    {
        IPerson person = GetPersons().FirstOrDefault(x => x.Id == personId);
        if (person == null) { return; }
        _db.Add(new Booking(_db.NextBookingId, vehicle, person));
        vehicle.Status = VehicleStatus.Booked;
    }

    public void ReturnVehicle(int vehicleID, double? distance)
    {
        if (distance == null) { return; }
        _db.ReturnVehicle(vehicleID, distance);
        distance = null;
    }

    public void RemoveCustomer(int? customerId)
    {
        _db.RemoveCustomer(customerId);
    }

    void ResetVehicleInputs()
    {
        regNo = string.Empty;
        brand = string.Empty;
        odometer = null;
        costKM = null;
        costDay = null;
        type = default;
    }
    void ResetCustomerInputs()
    {
        sSN = null;
        lastName = string.Empty;
        firstName = string.Empty;
    }

    bool CheckVehicleInputs(string regNo, string brand, double? odometer, double? costKM, double? costDay)
    {
        if(odometer == null || costKM == null || costDay == null) { return false; }
        if(string.IsNullOrEmpty(regNo) || string.IsNullOrEmpty(brand)) { return false; }
        if(costKM <= 0 || costDay <= 0) { return false; }
        if ($"{odometer}{costKM}{costDay}".Contains('e')) { return false; }
        foreach(IVehicle vehicle in GetVehicles())
        {
            if (vehicle.RegNo == regNo) { return false; }
        }
        return true;
    }

    bool CheckCustomerInputs(int? sSN, string? lastName, string? firstName)
    {
        if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || sSN == null) { return false; }
        if (sSN.ToString().Contains('e')) { return false; }
        foreach (IPerson person in GetPersons())
        {
            if (person.SSN == sSN) { return false; }
        }
        return true;
    }
}
