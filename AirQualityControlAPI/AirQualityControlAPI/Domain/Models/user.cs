﻿using System;
using System.Collections.Generic;

namespace AirQualityControlAPI.Domain.Models;

public partial class User
{
    public int User_id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Role_id { get; set; }

    public bool Is_active { get; set; }

    public string Phone { get; set; } = null!;
}
