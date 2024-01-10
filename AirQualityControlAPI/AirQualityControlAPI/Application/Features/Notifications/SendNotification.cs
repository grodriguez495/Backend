using System.Net;
using System.Net.Mail;
using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain.Enums;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AirQualityControlAPI.Application.Features.EmailNotifications;

public class SendNotification : ISendNotification
{
    private readonly IUserQueryRepository _userQueryRepository;

    public SendNotification(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
    }

    public async void SendEmailNotificationAsync(List<VariableValue> variableValues)
    {
        var admin = await _userQueryRepository.ListAsync(x =>
            x.RoleId == (int)RoleEnum.Admin && x.IsActive);
        var adminEmails = admin.Select(x => x.Email).ToList();
        foreach (var eachEmail in adminEmails)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("airqualitycontrolunab@gmail.com", "Air Quality Control UNAB");
            mailMessage.To.Add(new MailAddress(eachEmail));
            mailMessage.Subject = "Alerta: Variables de Calidad del aire fuera de los parametros";
            mailMessage.IsBodyHtml = false;
            mailMessage.Body = "mensaje de prueba";
            smtpClient.Credentials = new NetworkCredential("airqualitycontrolunab@gmail.com", "jdqeosapjlydkcym");
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
    }

    public async void SendSmsNotificationAsync(List<VariableValue> variableValues)
    {
        var originPhoneNumber = "+12068007332";
        var accountSID = "ACc51aa4ac22ceb49da74fb740c8e67a1f";
        var authToken = "66bd16d3f17dc4e82a9d8ff7554d70b4";
        var admin = await _userQueryRepository.ListAsync(x =>
            x.RoleId == (int)RoleEnum.Admin && x.IsActive);
        var adminPhoneNumbers = admin.Select(x => x).ToList();
        TwilioClient.Init(accountSID,authToken);
        
        foreach (var eachPhoneNumber in adminPhoneNumbers)
        {
             await MessageResource.CreateAsync(
                 body:"Alerta: Variables de Calidad del aire fuera de los parametros",
                 from: new Twilio.Types.PhoneNumber(originPhoneNumber),
                 to:new Twilio.Types.PhoneNumber(string.Concat("+57", eachPhoneNumber.Phone))
             );
        }
    }
}