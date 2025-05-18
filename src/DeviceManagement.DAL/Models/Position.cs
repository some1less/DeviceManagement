using System;
using System.Collections.Generic;

namespace DeviceManagement.DAL.Models;

public partial class Position
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int MinExpYears { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
