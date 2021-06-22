using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CqsDecorators
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped(typeof(IQueryHandler<,>), typeof(MainHandler<,>));

                    // Register decorators
                    services.AddDecorators((serviceProvider) =>
                    {
                        var decoratorFactory = new DecoratorFactory(serviceProvider);
                        decoratorFactory.Add(typeof(IQueryHandler<,>), typeof(LoggerHandler<,>));
                        decoratorFactory.Add(typeof(IQueryHandler<,>), typeof(TelemetryHandler<,>));
                        return decoratorFactory;
                    });
                })
                .UseConsoleLifetime()
                .Build();

            // Test
            var decoratorFactory = host.Services.GetRequiredService<IDecoratorFactory>();
            var decorator = decoratorFactory.BuildDecoratorsChain<IQueryHandler<DataQuery, string>>();
            var result = await decorator.HanldeAsync(new DataQuery { Page = 1 });

            Console.WriteLine(result.Data);
        }
    }
}
