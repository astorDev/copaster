using System.CommandLine;
using Copaster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection()
    .AddLogging(l =>
    {
        l.AddSimpleConsole(c => c.SingleLine = true);
        l.SetMinimumLevel(LogLevel.Trace);
    })
    .AddAppFolder("Copaster")
    .AddSingleton<CopyCommand>()
    .AddSingleton<PasteCommand>()
    .AddSingleton<RemoveCommand>()
    .AddSingleton<ConsoleClient>()
    .AddSingleton<ListCommand>()
    .BuildServiceProvider();

var logger = services.GetRequiredService<ILogger<Program>>();

var hostedServices = services.GetServices<IHostedService>();
foreach (var hostedService in hostedServices)
{
    await hostedService.StartAsync(CancellationToken.None);
}

var rootCommand = new RootCommand("Copaster CLI")
{
    services.GetRequiredService<CopyCommand>(),
    services.GetRequiredService<PasteCommand>(),
    services.GetRequiredService<RemoveCommand>(),
    services.GetRequiredService<ListCommand>()
};

await rootCommand.Parse(args).InvokeAsync();

await Task.Delay(50);

foreach (var hostedService in hostedServices.Reverse())
{
    await hostedService.StopAsync(CancellationToken.None);
}