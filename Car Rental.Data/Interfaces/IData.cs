using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Data.Interfaces;

public interface IData
{
    IEnumerable<IPerson> GetPersons();
    IEnumerable<IVehicle> GetVehicles(VehicleStatus status = default);
    IEnumerable<IBooking> GetBooking();

    int NextVehicleId { get; }
    int NextPersonId { get; }
    int NextBookingId { get; }
    public void Add<T>(T item);

    public void ReturnVehicle(int vehicleId, double? distance);
    public void RentVehicle(int vehicleId, int customerId);
    public void RemoveCustomer(int? customerId);

    //public List<T> Get<T>(Expression<Func<T, bool>>? expression);

    //T? Single<T>(Expression<Func<T, bool>>? expression);

    // Default Interface Methods
 /*public string[] VehicleStatusNames => Retunera enum konstanterna
 public string[] VehicleTypeNames => Retunera enum konstanterna
 public VehicleTypes GetVehicleTyp*/
}
