using AirQualityControlAPI.Application.Interfaces;
using AutoMapper;

namespace AirQualityControlAPI.Infrastructure.Persistence.Mapping
{
    public class AutoMapperAdapter : IAppMapper
    {
        private readonly IMapper _mapper;

        public AutoMapperAdapter(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TDestination>(object source, string key, object value)
        {
            return _mapper.Map<TDestination>(source, options => options.Items.Add(key, value));
        }

        public TDestination Map<TDestination>(object source, params (string key, object value)[] parameters)
        {
            return Map<TDestination>(source, parameters.ToDictionary(parameter => parameter.key, parameter => parameter.value));
        }

        public TDestination Map<TDestination>(object source, IDictionary<string, object> parameters)
        {
            return _mapper.Map<TDestination>(source, options =>
            {
                foreach (var (key, value) in parameters)
                    options.Items.Add(key, value);
            });
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return _mapper.Map(source, sourceType, destinationType);
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            return _mapper.Map(source, destination, sourceType, destinationType);
        }
    }
}
