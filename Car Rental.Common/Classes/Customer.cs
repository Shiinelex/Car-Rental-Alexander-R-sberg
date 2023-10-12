namespace Car_Rental.Common.Classes;

public class Customer : Person
{
    public Customer(int id, int? ssn, string? firstName, string? lastName)
    {
        Id = id;
        SSN = ssn;
        FirstName = firstName;
        LastName = lastName;
    }
}
