using Car_Rental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes;

public class Customer : IPerson
{
    public int SSN { get; init; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public Customer(int ssn, string? firstName, string? lastName)
    {
        this.SSN = ssn;
        this.FirstName = firstName;
        this.LastName = lastName;
    }
}
