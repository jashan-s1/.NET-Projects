using System;
using System.Collections.Generic;

namespace WebCoreAPI.Models;

public partial class Account
{
    public int AccountNumber { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public DateOnly? DateOfOpening { get; set; }

    public decimal? Amount { get; set; }

    public string? AccountType { get; set; }

    public string? Status { get; set; }
}
