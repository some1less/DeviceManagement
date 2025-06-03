using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.DAL.Models;

public partial class Person
{
    public int Id { get; set; }

    [Length(1,20)]
    public string PassportNumber { get; set; } = null!;

    [Length(1,20)]
    public string FirstName { get; set; } = null!;

    [Length(1,20)]
    public string? MiddleName { get; set; }

    [Length(1,20)]
    public string LastName { get; set; } = null!;

    [Length(1,20)]
    public string PhoneNumber { get; set; } = null!;

    [Length(1,50)]
    public string Email { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
