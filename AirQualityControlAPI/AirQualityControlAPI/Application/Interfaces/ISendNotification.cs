using AirQualityControlAPI.Domain.Models;

namespace AirQualityControlAPI.Application.Interfaces;

public interface ISendNotification
{
    Task  SendEmailNotificationAsync(VariableValue variableValues,CancellationToken cancellationToken);
    Task  SendSmsNotificationAsync(VariableValue variableValues,CancellationToken cancellationToken);
}