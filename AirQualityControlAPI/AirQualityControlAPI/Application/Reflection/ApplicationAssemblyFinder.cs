using System.Reflection;

namespace AirQualityControlAPI.Application.Reflection
{
    public static class ApplicationAssemblyFinder
    {
        public static Assembly GetAssembly() => typeof(ApplicationAssemblyFinder).Assembly;
    }
}
