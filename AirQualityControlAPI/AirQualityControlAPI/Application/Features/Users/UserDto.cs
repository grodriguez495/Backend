namespace AirQualityControlAPI.Application.Features.Users;

public class UserDto
{
    
    public int UserId { get; set; }

    public string Name { get; set; } 

    public string Password { get; set; } 

    public string Email { get; set; }

    public int RoleId { get; set; }

    public bool IsActive { get; set; }

    public string Phone { get; set; }

    public UserDto()
    {
    }

    public UserDto(int userId, string name, string password, string email, bool isActive, string phone)
    {
        UserId = userId;
        Name = name;
        Password = password;
        Email = email;
        IsActive = isActive;
        Phone = phone;
    }
}