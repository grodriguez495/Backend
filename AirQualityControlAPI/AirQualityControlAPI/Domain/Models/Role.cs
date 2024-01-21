using System;
using System.Collections.Generic;

namespace AirQualityControlAPI.Domain.Models;

public partial class Role : BaseEntity<int>
{
    public int RoleId { get; set; }

    public string Name { get; set; }
    

   
    public override int GetIdentity() => RoleId;

    public Role (int roleId, string name)
    {
        RoleId = roleId;
        Name = name;
    }

    public static Role NewRole(string name)
    {
        var entity = new Role(0, name);
        return entity;
    }
}
