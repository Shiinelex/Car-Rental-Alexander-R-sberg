using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    private readonly IData _db;

    public BookingProcessor(IData db) => _db = db;

    public List<IPerson> persons => GetPersons().ToList();
    public List<IBooking> bookings => GetBooking().ToList();
    public List<IVehicle> vehicles => GetVehicles().ToList();

    IEnumerable <IPerson> GetPersons()
    {
        IEnumerable<IPerson> p = _db.GetPersons();
        return p;
    }
    IEnumerable<IVehicle> GetVehicles(VehicleStatus status = default)
    {
        IEnumerable<IVehicle> v = _db.GetVehicles(status);
        return v;
    }
    IEnumerable<IBooking> GetBooking()
    {
        IEnumerable<IBooking> b = _db.GetBooking();

        foreach(var booking in b)
        {
            if (booking.IsOpen) { continue; }
            booking.CalculateCost();
        }
        return b;
    }
}
