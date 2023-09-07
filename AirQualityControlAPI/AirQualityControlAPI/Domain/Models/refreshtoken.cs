using System;
using System.Collections.Generic;

namespace AirQualityControlAPI.Domain.Models;

public partial class refreshtoken
{
    public int user_id { get; set; }

    public int token_id { get; set; }

    public string refresh_token { get; set; } = null!;
}
