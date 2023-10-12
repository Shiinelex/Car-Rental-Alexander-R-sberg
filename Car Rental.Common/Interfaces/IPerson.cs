namespace Car_Rental.Common.Interfaces;

public interface IPerson
{
    public int Id { get; set; }
    int? SSN { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

}
