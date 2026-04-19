using System.CommandLine;
using Copaster;

var services = new ServiceCollection()
    .AddLogging(l =>
    {
        l.AddSimpleConsole(c => c.SingleLine = true);
        l.SetMinimumLevel(LogLevel.Trace);
    })
    .AddSingleton<HereCommand>()
    .BuildServiceProvider();

var logger = services.GetRequiredService<ILogger<Program>>();

var hostedServices = services.GetServices<IHostedService>();
foreach (var hostedService in hostedServices)
{
    await hostedService.StartAsync(CancellationToken.None);
}

var rootCommand = new RootCommand("Copaster here/play CLI")
{
    services.GetRequiredService<HereCommand>()
};

await rootCommand.Parse(args).InvokeAsync();

foreach (var hostedService in hostedServices.Reverse())
{
    await hostedService.StopAsync(CancellationToken.None);
}