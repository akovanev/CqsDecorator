# CqsDecorator

## Preamble

While I was trying to use [Scrutor](https://github.com/khellang/Scrutor) for one of my projects, I got stuck on the [Open-generic registrations can not be decorated](https://github.com/khellang/Scrutor/issues/39) issue.

As Scrutor looks very attractive to me and I wouldn't like to give up, I have desided to find a workaround.

## Important

This project is far from ideal, even without checking the arguments, it is just an attempt to point the direction.

## Usage example

```csharp
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

```
