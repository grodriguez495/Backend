using System.Reflection;

namespace AirQualityControlAPI.Application.Reflection;

public class PersistenceAssemblyFinder
{
    public static Assembly GetAssembly() => typeof(PersistenceAssemblyFinder).Assembly;
}