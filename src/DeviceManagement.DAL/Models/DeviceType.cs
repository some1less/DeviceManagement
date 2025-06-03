using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.DAL.Models;

public partial class DeviceType
{
    public int Id { get; set; }

    [Length(1,25)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
