using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Common.Classes;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Data.Classes;

public class CollectionData : IData
{
    readonly List<IPerson> persons = new List<IPerson>();
    readonly List<IVehicle> vehicles = new List<IVehicle>();
    readonly List<IBooking> bookings = new List<IBooking>();

    public CollectionData() => SeedData();

    void SeedData()
    {
        persons.Add(new Customer(7777, "Captain", "Falcon"));
        persons.Add(new Customer(1010, "James", "McLoud"));
        persons.Add(new Customer(3636, "Princia", "Remode"));
        vehicles.Add(new Car("AAA111", "Volvo", 0, 100, VehicleType.Sedan, 2500));
        vehicles.Add(new Car("BBB222", "Volkswagen", 150, 200, VehicleType.Van, 3000, VehicleStatus.Booked));
        vehicles.Add(new Motorcycle("CCC333", "Volvo", 300, 300, VehicleType.Motorcycle, 3500, VehicleStatus.Booked));
        bookings.Add(new Booking(vehicles[1], persons[1], null, new DateTime(2015, 12, 23), new DateTime(2015, 12, 25), true));
        bookings.Add(new Booking(vehicles[2], persons[2], 600, new DateTime(2015, 12, 20), new DateTime(2015, 12, 25), false));
    }

    public IEnumerable<IPerson> GetPersons() => persons;
    public IEnumerable<IVehicle> GetVehicles(VehicleStatus status = default) => vehicles;
    public IEnumerable<IBooking> GetBooking() => bookings;
}
