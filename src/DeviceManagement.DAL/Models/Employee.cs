using System;
using System.Collections.Generic;

namespace DeviceManagement.DAL.Models;

public partial class Employee
{
    public int Id { get; set; }

    public decimal Salary { get; set; }

    public int PositionId { get; set; }

    public int PersonId { get; set; }

    public DateTime HireDate { get; set; }

    public virtual ICollection<DeviceEmployee> DeviceEmployees { get; set; } = new List<DeviceEmployee>();

    public virtual Person Person { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;
}
