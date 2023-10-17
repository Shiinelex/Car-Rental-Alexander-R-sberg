using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Extensions;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

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
        persons.Add(new Customer(NextPersonId, 7777, "Captain", "Falcon"));
        persons.Add(new Customer(NextPersonId, 1010, "James", "McLoud"));
        persons.Add(new Customer(NextPersonId, 3636, "Princia", "Remode"));
        vehicles.Add(new Car(NextVehicleId, "AAA111", "Volvo", 1500, 2, VehicleType.Combi, 10));
        vehicles.Add(new Car(NextVehicleId, "BBB222", "Bugatti", 100, 100, VehicleType.Sport, 1000));
        vehicles.Add(new Car(NextVehicleId, "CCC333", "Hemmabygge", 6500, 0.01, VehicleType.Motorcycle, 1));
    }

    public List<T> Get<T>(Expression<Func<T, bool>>? expression)
    {
        Type myType = typeof(CollectionData);

        FieldInfo[] fieldInfo = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        foreach(var field in fieldInfo)
        {
            if (typeof(List<T>) == field.FieldType)
            {
                List<T> list = (List<T>)field.GetValue(this);
                var result = expression != null ?
                list.Cast<T>().Where(expression.Compile()).ToList()
                : list.Cast<T>().ToList();
                return result;
            }
            continue;
        }

        throw new ArgumentException("Finns ej");
    }

    public T? Single<T>(Expression<Func<T, bool>>? expression)
    {
        Type myType = typeof(CollectionData);

        FieldInfo[] fieldInfo = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var field in fieldInfo)
        {
            if (typeof(List<T>) == field.FieldType)
            {
                List<T> list = (List<T>)field.GetValue(this);
                var result = expression != null ?
                list.Where(expression.Compile()).Single()
                : list.Single();
                return result;
            }
            continue;
        }

        throw new ArgumentException("Finns ej");
    }

    public void Add<T>(T item)
    {
        Type myType = typeof(CollectionData);

        var fieldInfo = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        if(item == null) { return ; }

        foreach (var field in fieldInfo)
        {            
            if (typeof(List<T>) == field.FieldType)
            {
                var list = field.GetValue(this) as List<T>;

                if(list != null)
                list.Add(item);

                return;
            }
            continue;
        }

        return;
    }

    public void RentVehicle(int vehicleId, int customerId)
    {
        var vehicle = Single<IVehicle>(v => v.Id == vehicleId);
        var customer = Single<IPerson>(p => p.Id == customerId);
        var booking = new Booking(NextBookingId, vehicle, customer);
        Add(booking);
    }
    public void RemoveCustomer(int? customerId)
    {
        var customer = customerId != null ? Single<IPerson>(p => p.Id == customerId)
            : throw new ArgumentException("Customer id är Null");
        persons.Remove(customer);
    }
    public void ReturnVehicle(int vehicleID, double? distance)
    {
            IVehicle vehicle = vehicles.Single(v => v.Id == vehicleID);
            IBooking booking = bookings.Single(b => b.RegNo == vehicle.RegNo && b.Status == BookingStatus.Open);
            vehicle.Status = VehicleStatus.Available;
            booking.Status = BookingStatus.Closed;
            booking.DateReturned = DateTime.Now;
            booking.KmReturned = booking.KmRented + distance;
            booking.Cost = (booking.DateReturned.Duration(booking.DateRented) * booking.CostDay)
                           + (booking.KmReturned.TravelDistance(booking.KmRented) * booking.CostKM);
            vehicle.Odometer = booking.KmReturned;
    }
}