using AutoMapper;
using System;
using System.Linq;

namespace SportsTrading.Web.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            System.Collections.Generic.List<Type> types = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.Contains(nameof(SportsTrading)))
                .SelectMany(a => a.GetTypes())
                .ToList();

            var mappings = types
                .Where(t => t.IsClass &&
                            !t.IsAbstract &&
                            t
                                .GetInterfaces()
                                .Any(i => i.IsGenericType &&
                                          i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .Select(t => new
                {
                    Destination = t,
                    Source = t
                        .GetInterfaces()
                        .FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                        .GetGenericArguments()
                        .First()
                })
                .ToList();


            foreach (var mapping in mappings)
            {
                this.CreateMap(mapping.Source, mapping.Destination);
            }

            System.Collections.Generic.List<ICustomMapping> customMappings = types
                .Where(t => t.IsClass &&
                            !t.IsAbstract &&
                            typeof(ICustomMapping).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<ICustomMapping>()
                .ToList();

            foreach (ICustomMapping customMapping in customMappings)
            {
                customMapping.ConfigureMapping(this);
            }
        }
    }
}
