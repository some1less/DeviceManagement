using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.DAL.Models;

public partial class Device
{
    public int Id { get; set; }

    [Length(1,50)]
    public string Name { get; set; } = null!;

    public bool IsEnabled { get; set; }
    
    [Length(1,100)]
    public string AdditionalProperties { get; set; } = null!;

    public int? DeviceTypeId { get; set; }

    public virtual ICollection<DeviceEmployee> DeviceEmployees { get; set; } = new List<DeviceEmployee>();

    public virtual DeviceType? DeviceType { get; set; }
}
