using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Copaster;

public record AppFolder(string Path) : Folder(Path)
{
    public static AppFolder ForApp(string appName)
    {
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolderPath = System.IO.Path.Combine(appDataFolder, appName);
        return new AppFolder(appFolderPath);
    }
}

public class AppFolderInitializer(AppFolder appFolder, ILogger<AppFolderInitializer> logger) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogDebug("Ensuring application folder exists at: {folder}", appFolder.Path);
        appFolder.EnsureExists();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public static class AppFolderExtensions
{
    public static IServiceCollection AddAppFolder(this IServiceCollection services, string appName)
    {
        var appFolder = AppFolder.ForApp(appName);
        services.AddSingleton(appFolder);
        services.AddHostedService<AppFolderInitializer>();
        return services;
    }
}