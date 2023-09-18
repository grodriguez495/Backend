using System;
using System.Collections.Generic;
using AirQualityControlAPI.Application.Features.Roles;

namespace AirQualityControlAPI.Domain.Models;

public partial class User  : BaseEntity<int>
{
    public int UserId { get; set; }

    public string Name { get; set; } 

    public string Password { get; set; } 

    public string Email { get; set; }

    public int RoleId { get; set; }

    public bool IsActive { get; set; }

    public string Phone { get; set; }

    public virtual Role? Role { get; set; }

    public override int GetIdentity() => UserId;

    public User(int userId,string name, string password, string email, int roleId, bool isActive, string phone)
    {
        UserId = userId;
        Name = name;
        Password = password;
        Email = email;
        RoleId = roleId;
        IsActive = isActive;
        Phone = phone;
    }

    public static User NewUser(string name, string password, string email, int roleId, bool isActive, string phone)
    {
        var entity = new User(0, name, password, email, roleId, isActive, phone);
        return entity;
    }
}
