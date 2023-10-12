using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    private readonly IData _db;



    public BookingProcessor(IData db) => _db = db;

    public Vehicle tempVehicle = new();
    public Person tempCustomer = new();

    public bool isLoading = false;

    #region Variables Booking
    public double? personId;
    public double? KmReturned;
    public double? Cost;

    int selectNumber = 0;

    public double? distance;
    #endregion

    #region Get Methods
    public IEnumerable<IPerson> GetPersons()
    {
        IEnumerable<IPerson> personList = _db.Get<IPerson>(p => p != null);
        return personList;
    }
    public IEnumerable<IVehicle> GetVehicles()
    {
        IEnumerable<IVehicle> vehicleList = _db.Get<IVehicle>(v => v != null);
        return vehicleList;
    }
    public IEnumerable<IBooking> GetBookings()
    {
        IEnumerable<IBooking> bookingList = _db.Get<IBooking>(b => b != null);
        return bookingList;
    }
    #endregion

    public void AddPerson()
    {
        if (!CheckCustomerInputs(tempCustomer.SSN, tempCustomer.LastName, tempCustomer.FirstName)) { return; }
        _db.Add(new Customer(_db.NextPersonId, tempCustomer.SSN, tempCustomer.FirstName, tempCustomer.LastName));
        tempCustomer = new();
    }
    public void AddVehicle()
    {

        if (!CheckVehicleInputs(tempVehicle.RegNo, tempVehicle.Brand, tempVehicle.Odometer, tempVehicle.CostKM, tempVehicle.CostDay)) { return; }
        if (tempVehicle.Type == VehicleType.Motorcycle)
        {
            _db.Add(new Motorcycle(_db.NextVehicleId, tempVehicle.RegNo, tempVehicle.Brand, tempVehicle.Odometer, tempVehicle.CostKM, tempVehicle.CostDay));
            tempVehicle = new();
        }
        else
        {
            _db.Add(new Car(_db.NextVehicleId, tempVehicle.RegNo, tempVehicle.Brand, tempVehicle.Odometer, tempVehicle.CostKM, tempVehicle.Type, tempVehicle.CostDay));
            tempVehicle = new();
        }
    }
    public async Task RentVehicle(IVehicle vehicle)
    {
        await WaitAWhile();
        IPerson person = GetPersons().FirstOrDefault(x => x.Id == vehicle.tempPerson);
        if (person == null) { return; }
        _db.Add(new Booking(_db.NextBookingId, vehicle, person));
        vehicle.Status = VehicleStatus.Booked;
    }

    public void ReturnVehicle(int vehicleID, double? dist)
    {
        if (dist == null) { return; }
        _db.ReturnVehicle(vehicleID, dist);
        distance = null;
    }

    public void RemoveCustomer(int? customerId)
    {
        _db.RemoveCustomer(customerId);
    }

    public async Task WaitAWhile()
    {
        isLoading = true;
        await Task.Delay(1000);
        isLoading = false;
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
