using System.CommandLine;
using Microsoft.Extensions.Logging;

namespace Copaster;

public class RemoveCommand : Command
{
    static readonly Argument<string> NameArgument = new(
        name: "name"
    )
    {
        Description = "The name of the template to be removed. If not specified, the file name will be used."
    };

    private readonly ILogger<RemoveCommand> logger;
    private readonly AppFolder appFolder;
    private readonly ConsoleClient consoleClient;

    public RemoveCommand(
        AppFolder appFolder, 
        ILogger<RemoveCommand> logger,
        ConsoleClient consoleClient) : base("remove", "Remove the specified template from the central registry")
    {
        this.logger = logger;
        this.appFolder = appFolder;
        this.consoleClient = consoleClient;

        Add(NameArgument);
    
        SetAction(async (parsed) =>
        {
            var templateName = parsed.GetRequiredValue(NameArgument);

            await RemoveTemplate(templateName);
        });
    }

    public async Task RemoveTemplate(string name)
    {
        var templateFolder = appFolder.Subfolder(name);

        logger.LogDebug("Removing template folder {folder}", templateFolder.Path);
        templateFolder.Delete();
        logger.LogInformation("Template folder {folder} removed", templateFolder.Path);

        logger.LogDebug("Removing dotnet new template with name {name}", name);
        await consoleClient.ExecuteAsync($"dotnet new uninstall \"{templateFolder.Path}\"");
        logger.LogInformation("Dotnet new template with name {name} uninstalled", name);
    }
}