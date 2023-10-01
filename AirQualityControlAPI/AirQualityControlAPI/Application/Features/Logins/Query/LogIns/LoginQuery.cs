using MediatR;

namespace AirQualityControlAPI.Application.Features.Logins.Query.LogIns;

public class LoginQuery  : IRequest<LoginDto?>
{
   public string Email { get; set; }
   public string Password { get; set; }

   public LoginQuery(string email, string password)
   {
      Email = email;
      Password = password;
   }
}