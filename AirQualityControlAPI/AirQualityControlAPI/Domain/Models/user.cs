using System;
using System.Collections.Generic;

namespace AirQualityControlAPI.Domain.Models;

public partial class user
{
    public int user_id { get; set; }

    public string name { get; set; } = null!;

    public string password { get; set; } = null!;

    public string email { get; set; } = null!;

    public int role_id { get; set; }

    public bool is_active { get; set; }

    public string phone { get; set; } = null!;
}
