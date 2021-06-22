using System;

using Microsoft.Extensions.DependencyInjection;

namespace CqsDecorators
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDecorators(
            this IServiceCollection services,
            Func<IServiceProvider, IDecoratorFactory> implementationFactory)
        {
            // Register as singleton
            services.AddSingleton(svc => implementationFactory(svc));

            return services;
        }
    }
}
