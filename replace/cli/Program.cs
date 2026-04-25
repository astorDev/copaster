using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging(l => l.AddSimpleConsole(c => c.SingleLine = true));
var services = serviceCollection.BuildServiceProvider();

var logger = services.GetRequiredService<ILogger<Program>>();

var currentDirectory = Directory.GetCurrentDirectory();
logger.LogInformation("Current directory: {CurrentDirectory}", currentDirectory);

await Task.Delay(1); // let the logger print the message before the application exits