using System;
using System.Collections.Generic;

namespace DeviceManagement.DAL.Models;

public partial class Person
{
    public int Id { get; set; }

    public string PassportNumber { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
