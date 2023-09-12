using System;
using System.Collections.Generic;

namespace AirQualityControlAPI.Domain.Models;

public partial class Role : BaseEntity<int>
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public override int GetIdentity() => RoleId;
}
