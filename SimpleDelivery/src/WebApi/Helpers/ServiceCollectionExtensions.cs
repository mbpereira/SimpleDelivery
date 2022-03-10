using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebApi.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static void AddScopedTypesByDefaultConvention(this IServiceCollection services, Assembly src)
        {
            var typesWithInterfaces = src.GetTypes().Where(t => t.GetInterfaces().Length > 0);
            var typesToRegister = new Dictionary<Type, Type>();

            foreach (var type in typesWithInterfaces)
            {
                var name = $"I{type.Name}".ToUpperInvariant();
                var definition = type.GetInterfaces().FirstOrDefault(i => i.Name.ToUpperInvariant().Equals(name));
                if (definition != null)
                    typesToRegister.Add(definition, type);
            }

            foreach (var (definition, implementation) in typesToRegister)
                services.AddScoped(definition, implementation);
        }
    }
}
