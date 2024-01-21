using System.Net;
using System.Net.Mail;
using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain.Enums;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Alerts.Commands;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AirQualityControlAPI.Application.Features.EmailNotifications;

public class SendNotification : ISendNotification
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IAlertsCommandRepository _alertsCommandRepository;

    public SendNotification(IUserQueryRepository userQueryRepository, IAlertsCommandRepository alertsCommandRepository)
    {
        _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
        _alertsCommandRepository = alertsCommandRepository ?? throw new ArgumentNullException(nameof(alertsCommandRepository));
    }

    public async Task SendEmailNotificationAsync(VariableValue variableValues,CancellationToken cancellationToken)
    {
        var admin = await _userQueryRepository.ListAsync(x =>
            x.RoleId == (int)RoleEnum.Admin && x.IsActive,false,cancellationToken);
        var adminEmails = admin.Select(x => x.Email).ToList();
        foreach (var eachEmail in adminEmails)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("airqualitycontrolunab@gmail.com", "Air Quality Control UNAB");
            mailMessage.To.Add(new MailAddress(eachEmail));
            mailMessage.Subject = $"Alerta: La Variable {variableValues.VariableName} fuera de los parametros";
            mailMessage.IsBodyHtml = false;
            mailMessage.Body = $"La variable {variableValues.VariableName} se encuentra dentro de la clasificación: {variableValues.Clasificacion}. Con el valor ICA: {variableValues.Value} ";
            smtpClient.Credentials = new NetworkCredential("airqualitycontrolunab@gmail.com", "jdqeosapjlydkcym");
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
            var alert = AlertNotification.NewAlerts(DateTime.Now, mailMessage.Body, eachEmail, (int)AlertTypeEnum.Email,false);
            await _alertsCommandRepository.RegisterAsync(alert, cancellationToken);
        }
    }

    public async Task SendSmsNotificationAsync(VariableValue variableValues,CancellationToken cancellationToken)
    {
        try
        {
            var originPhoneNumber = "+12068007332";
            var accountSID = "ACc51aa4ac22ceb49da74fb740c8e67a1f";
            var authToken = "66bd16d3f17dc4e82a9d8ff7554d70b4";
            var admin = await _userQueryRepository.ListAsync(x =>
                x.RoleId == (int)RoleEnum.Admin && x.IsActive,false,cancellationToken);
            var adminPhoneNumbers = admin.Select(x => x).ToList();
            TwilioClient.Init(accountSID, authToken);


            foreach (var eachPhoneNumber in adminPhoneNumbers)
            {
                var completeNumber = string.Concat("+57", eachPhoneNumber.Phone);
                var messageOptions = new CreateMessageOptions(
                    new PhoneNumber(completeNumber));
                messageOptions.From = new PhoneNumber(originPhoneNumber);

                messageOptions.Body =
                    $"La variable {variableValues.VariableName} se encuentra dentro de la clasificación: {variableValues.Clasificacion}. Con el valor ICA: {variableValues.Value} ";

                var alert = AlertNotification.NewAlerts(DateTime.Now, messageOptions.Body, completeNumber, (int)AlertTypeEnum.Sms,false);
                await _alertsCommandRepository.RegisterAsync(alert, cancellationToken);
                MessageResource.Create(messageOptions);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("error:" +ex.Message);
        }


    }
}