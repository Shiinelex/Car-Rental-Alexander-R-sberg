namespace Car_Rental.Common.Interfaces;

public interface IPerson
{
    int SSN { get; init; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

}
