namespace AirQualityControlAPI.Domain
{
    public interface IAuditableEntity : IEntity
    {
        DateTime? Created { get; set; }
        DateTime? LastUpdated { get; set; }
        string CreatedBy { get; set; }
        string LastUpdatedBy { get; set; }
    }
}
