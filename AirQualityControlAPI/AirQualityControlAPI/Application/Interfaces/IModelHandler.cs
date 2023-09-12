using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Application.Interfaces
{
    public interface IModelHandler
    {
        void OnModelCreatingFromContext(ModelBuilder modelBuilder);
        bool ModelCreated { get; set; }
    }
}
