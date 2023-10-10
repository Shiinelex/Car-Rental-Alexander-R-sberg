﻿namespace Car_Rental.Common.Interfaces;

public interface IPerson
{
    public int Id { get; init; }
    int? SSN { get; init; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

}
