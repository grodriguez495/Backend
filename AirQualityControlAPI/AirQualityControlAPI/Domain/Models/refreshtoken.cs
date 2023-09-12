using System;
using System.Collections.Generic;

namespace AirQualityControlAPI.Domain.Models;

public partial class Refreshtoken
{
    public int User_id { get; set; }

    public int Token_id { get; set; }

    public string Refresh_token { get; set; } = null!;
}
