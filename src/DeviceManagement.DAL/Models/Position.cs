using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.DAL.Models;

public partial class Position
{
    public int Id { get; set; }

    [Length(1,30)]
    public string Name { get; set; } = null!;

    public int MinExpYears { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
