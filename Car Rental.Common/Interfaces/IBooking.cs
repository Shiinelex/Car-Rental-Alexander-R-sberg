namespace Car_Rental.Common.Interfaces;

public interface IBooking
{
    public string RegNo { get; init; }
    public string Customer { get; init; }
    public int KmRented { get; set; }
    public int? KmReturned { get; set; }
    public DateTime DateRented { get; init; }
    public DateTime DateReturned { get; set; }
    public double? Cost { get; set; }
    public double CostKM { get; set; }
    public double CostDay { get; set; }
    public bool IsOpen { get; set; }


    public void CalculateCost();
}
