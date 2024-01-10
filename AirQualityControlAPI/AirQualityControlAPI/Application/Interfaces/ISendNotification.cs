using AirQualityControlAPI.Domain.Models;

namespace AirQualityControlAPI.Application.Interfaces;

public interface ISendNotification
{
    void SendEmailNotificationAsync(List<VariableValue> variableValues);
    void SendSmsNotificationAsync(List<VariableValue> variableValues);
}