using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    private readonly IData _db;

    public string error = string.Empty;

    public BookingProcessor(IData db) => _db = db;

    public Vehicle tempVehicle = new();
    public Person tempCustomer = new();

    public bool isLoading = false;

    #region Variables Booking
    public double? personId;
    public double? KmReturned;
    public double? Cost;

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
        if (!CheckCustomerInputs(tempCustomer.SSN, tempCustomer.LastName, tempCustomer.FirstName)) 
        {
            return; 
        }
        IPerson person = new Customer(_db.NextPersonId, tempCustomer.SSN, tempCustomer.FirstName, tempCustomer.LastName);
        _db.Add(person);
        tempCustomer = new();
    }
    public void AddVehicle()
    {
        if (!CheckVehicleInputs(tempVehicle.RegNo, tempVehicle.Brand, tempVehicle.Odometer, tempVehicle.CostKM, tempVehicle.CostDay)) 
        {
            return; 
        }
        IVehicle vehicle;
        if (tempVehicle.Type == VehicleType.Motorcycle)
        {
            vehicle = new Motorcycle(_db.NextVehicleId, tempVehicle.RegNo, tempVehicle.Brand, tempVehicle.Odometer, tempVehicle.CostKM, tempVehicle.CostDay);
        }
        else
        {
            vehicle = new Car(_db.NextVehicleId, tempVehicle.RegNo, tempVehicle.Brand, tempVehicle.Odometer, tempVehicle.CostKM, tempVehicle.Type, tempVehicle.CostDay);
        }
        _db.Add(vehicle);
        tempVehicle = new();
    }
    public async Task RentVehicle(IVehicle vehicle)
    {
        await WaitAWhile();
        IPerson person = GetPersons().FirstOrDefault(x => x.Id == vehicle.tempPerson);
        if (person == null) { return; }
        IBooking booking = new Booking(_db.NextBookingId, vehicle, person);
        _db.Add(booking);
        vehicle.Status = VehicleStatus.Booked;
    }

    public void ReturnVehicle(int vehicleID, double? dist)
    {
        try
        {
            if (dist == null) { return; }
            _db.ReturnVehicle(vehicleID, dist);
            distance = null;
        }
        catch
        {
            error = "Return Failed";
        }

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
        if ($"{odometer}{costKM}{costDay}".Contains('e'))
        {
            error = "Odometer, CostKM and $Day shuld only contain numbers";
            return false;
        }
        if (odometer == null || costKM == null || costDay == null) 
        {
            error = "Every input is not filled in";
            return false; 
        }
        if(string.IsNullOrEmpty(regNo) || string.IsNullOrEmpty(brand)) 
        {
            error = "Every input is not filled in";
            return false; 
        }
        if(costKM <= 0 || costDay <= 0) 
        {
            error = "CostKM and $Day should not be zero";
            return false; 
        }
        foreach(IVehicle vehicle in GetVehicles())
        {
            if (vehicle.RegNo == regNo) 
            {
                error = "Reg Number already exist";
                return false; 
            }
        }
        if(odometer < 0 || costKM < 0 || costDay < 0)
        {
            error = "Odometer, CostKM and $Day cannot be less than 0";
            return false;
        }
        error = string.Empty;
        return true;
    }

    bool CheckCustomerInputs(int? sSN, string? lastName, string? firstName)
    {
        if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || sSN == null) 
        { 
            error = "Every input is not filled in"; 
            return false; 
        }
        if (sSN.ToString().Contains('e') || sSN.ToString().Length != 4) 
        { 
            error = "SSN should only contain 4 numbers"; 
            return false; 
        }
        foreach (IPerson person in GetPersons())
        {
            if (person.SSN == sSN) { error = "SSN elready exist"; return false; }
        }
        if (lastName.Any(char.IsDigit))
        {
            error = "The last name contains numbers"; 
            return false;
        }
        if (firstName.Any(char.IsDigit))
        {
            error = "The first name contains numbers";
            return false;
        }

        error = string.Empty;
        return true;
    }
}
