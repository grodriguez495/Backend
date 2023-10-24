using MediatR;

namespace AirQualityControlAPI.Application.Features.Logins.Commands.LogIns;

public class LoginCommand  : IRequest<LoginDto?>
{
   public string Email { get; set; }
   public string Password { get; set; }

   public LoginCommand(string email, string password)
   {
      Email = email;
      Password = password;
   }
}