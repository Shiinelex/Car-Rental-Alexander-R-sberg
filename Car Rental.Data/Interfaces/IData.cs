using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System.Linq.Expressions;

namespace Car_Rental.Data.Interfaces;

public interface IData
{
    int NextVehicleId { get; }
    int NextPersonId { get; }
    int NextBookingId { get; }
    public void Add<T>(T item);

    public void ReturnVehicle(int vehicleId, double? distance);
    public void RentVehicle(int vehicleId, int customerId);
    public void RemoveCustomer(int? customerId);

    public List<T> Get<T>(Expression<Func<T, bool>>? expression);

    public T? Single<T>(Expression<Func<T, bool>>? expression);
}
