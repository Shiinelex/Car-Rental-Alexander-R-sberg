using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Data.Interfaces;

public interface IData
{
    IEnumerable<IPerson> GetPersons();
    IEnumerable<IVehicle> GetVehicles(VehicleStatus status = default);
    IEnumerable<IBooking> GetBooking();

}
