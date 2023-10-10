using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Common.Classes;
using Car_Rental.Data.Interfaces;
using Car_Rental.Common.Extensions;

namespace Car_Rental.Data.Classes;

public class CollectionData : IData
{
    readonly List<IPerson> persons = new List<IPerson>();
    readonly List<IVehicle> vehicles = new List<IVehicle>();
    readonly List<IBooking> bookings = new List<IBooking>();

    public CollectionData() => SeedData();

    #region ids
    public int NextVehicleId => vehicles.Count == 0 ? 1 : vehicles.Max(b => b.Id) + 1;
    public int NextPersonId => persons.Count == 0 ? 1 : persons.Max(b => b.Id) + 1;
    public int NextBookingId => bookings.Count == 0 ? 1 : bookings.Max(b => b.Id) + 1;
    #endregion

    void SeedData()
    {
        Add(new Customer(NextPersonId, 7777, "Captain", "Falcon"));
        Add(new Customer(NextPersonId, 1010, "James", "McLoud"));
        Add(new Customer(NextPersonId, 3636, "Princia", "Remode"));
        Add(new Car(NextVehicleId, "AAA111", "Volvo", 1500, 2, VehicleType.Combi, 10));
        Add(new Car(NextVehicleId, "BBB222", "Bugatti", 100, 100, VehicleType.Sport, 1000));
        Add(new Car(NextVehicleId, "CCC333", "Hemmabygge", 6500, 0.01, VehicleType.Motorcycle, 1));
    }

    public IEnumerable<IPerson> GetPersons() => persons;
    public IEnumerable<IVehicle> GetVehicles(VehicleStatus status = default) => vehicles;
    public IEnumerable<IBooking> GetBooking() => bookings;
    //public List<T> Get<T>(Expression<Func<T, bool>>? expression)

    public void Add<T>(T item)
    {
        if (item == null) { return; }
        if (typeof(IPerson).IsAssignableFrom(item.GetType())) { persons.Add((IPerson)item); }
        if (typeof(IVehicle).IsAssignableFrom(item.GetType())) { vehicles.Add((IVehicle)item); }
        if (typeof(IBooking).IsAssignableFrom(item.GetType())) { bookings.Add((IBooking)item); }
    }

    public void RentVehicle(int vehicleId, int customerId)
    {
        var vehicle = GetVehicles().Single(v => v.Id == vehicleId);
        var customer = GetPersons().Single(p => p.Id == customerId);
        var booking = new Booking(NextBookingId, vehicle, customer);
        Add(booking);
    }
    public void RemoveCustomer(int? customerId)
    {
        if (customerId == null) { return; }
        var customer = GetPersons().Single(p => p.Id == customerId);
        persons.Remove(customer);
    }
    public void ReturnVehicle(int vehicleID, double? distance)
    {
        var vehicle = vehicles.Single(v => v.Id == vehicleID);
        var booking = bookings.Single(b => b.RegNo == vehicle.RegNo && b.Status == BookingStatus.Open);
        vehicle.Status = VehicleStatus.Available;
        booking.Status = BookingStatus.Closed;
        booking.DateReturned = DateTime.Now;
        booking.KmReturned = booking.KmRented + distance;
        booking.Cost = (booking.DateReturned.Duration(booking.DateRented) * booking.CostDay)
                       + (booking.KmReturned.TravelDistance(booking.KmRented) * booking.CostKM);
        vehicle.Odometer = booking.KmReturned;
    }
}
