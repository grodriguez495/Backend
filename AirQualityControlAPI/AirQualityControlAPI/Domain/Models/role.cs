using System;
using System.Collections.Generic;

namespace AirQualityControlAPI.Domain.Models;

public partial class role
{
    public int roleId { get; set; }

    public string Name { get; set; } = null!;
}
