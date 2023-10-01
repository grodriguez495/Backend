namespace AirQualityControlAPI.Application.Features.Logins;

public class LoginDto
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string RoleId { get; set; }
    public bool IsActive { get; set; }
    public string Phone { get; set; }
}