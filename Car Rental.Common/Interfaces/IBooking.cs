using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IBooking
{
    public int Id { get; init; }
    public string RegNo { get; init; }
    public string Customer { get; init; }
    public double? KmRented { get; set; }
    public double? KmReturned { get; set; }
    public DateTime DateRented { get; init; }
    public DateTime DateReturned { get; set; }
    public double? Cost { get; set; }
    public double? CostKM { get; set; }
    public double? CostDay { get; set; }
    public BookingStatus Status { get; set; }
}
