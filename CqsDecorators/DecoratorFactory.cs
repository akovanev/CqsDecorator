using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace CqsDecorators
{
    public class DecoratorFactory : IDecoratorFactory
    {
        private readonly Dictionary<Type, List<Type>> types = new Dictionary<Type, List<Type>>();

        private readonly IServiceProvider serviceProvider;

        public DecoratorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Add(Type service, Type decorator)
        {
            if (types.ContainsKey(service))
            {
                types[service].Add(decorator);
            }
            else
            {
                types.Add(service, new List<Type> { decorator });
            }
        }

        public T BuildDecoratorsChain<T>()
        {
            var type = typeof(T);
            var key = type.GetGenericTypeDefinition();

            if (!types.ContainsKey(key))
            {
                return default;
            }

            var service = serviceProvider.GetRequiredService<T>();
            var args = type.GetGenericArguments();

            foreach (var value in types[key])
            {
                var decorator = value.MakeGenericType(args);
                var parameters = BuildConstructorParameters(service, decorator);
                service = (T)Activator.CreateInstance(decorator, parameters.ToArray());
            }

            return service;
        }

        private object[] BuildConstructorParameters<T>(T service, Type decorator)
        {
            var result = new List<object>();
            foreach (var parameter in decorator.GetConstructors().Single().GetParameters())
            {
                var parameterType = parameter.ParameterType;
                if(parameterType.IsAssignableFrom(service.GetType()))
                {
                    result.Add(service);
                }
                else
                {
                    result.Add(serviceProvider.GetRequiredService(parameterType));
                }    
            }

            return result.ToArray();
        }
    }
}
