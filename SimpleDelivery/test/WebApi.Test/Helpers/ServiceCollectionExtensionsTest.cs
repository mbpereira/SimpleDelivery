using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApi.Helpers;
using WebApi.Test.Helpers.Context;
using WebApi.Test.Helpers.Context.Bar;
using WebApi.Test.Helpers.Context.Foo;
using Xunit;

namespace WebApi.Test.Helpers
{
    public class ServiceCollectionExtensionsTest
    {
        [Fact]
        public void ShouldRegisterTypesWhichImplementsAnInterfaceByDefaultConventionAndIgnoreOthers()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddScopedTypesByDefaultConvention(typeof(ServiceCollectionExtensionsTest).Assembly);

            var registeredTypeNames = new string[]
            {
                typeof(Quux).Name,
                typeof(Bar).Name,
                typeof(Foo).Name
            };

            var notRegisteredTypeNames = new string[] 
            { 
                typeof(Baz).Name 
            };

            var notRegisteredTypes = serviceCollection
                .Where(s => notRegisteredTypeNames.Contains(s.ImplementationType.Name)).ToList();

            var registeredTypes = serviceCollection
                .Where(s => registeredTypeNames.Contains(s.ImplementationType.Name)).ToList();

            Assert.Equal(3, registeredTypes.Count);
            Assert.Empty(notRegisteredTypes);
        }
    }
}
