using System.CommandLine;
using Microsoft.Extensions.Logging;

namespace Copaster;

public class CopyCommand : Command
{
    static readonly Argument<string> SourceFileArgument = new(
        name: "source"
    );

    private readonly ILogger<CopyCommand> logger;
    private readonly AppFolder appFolder;
    private readonly ConsoleClient consoleClient;

    public CopyCommand(
        AppFolder appFolder, 
        ILogger<CopyCommand> logger,
        ConsoleClient consoleClient) : base("copy", "Copy a file to the application folder")
    {
        this.logger = logger;
        this.appFolder = appFolder;
        this.consoleClient = consoleClient;
    
        Add(SourceFileArgument);

        SetAction(async (parsed) =>
        {
            var sourceFilePath = parsed.GetRequiredValue(SourceFileArgument);

            var templateFolder = GenerateTemplateFolder(sourceFilePath);
            await InstallTemplate(templateFolder);
        });
    }

    Folder GenerateTemplateFolder(string sourceFilePath)
    {
        logger.LogDebug("Generating template folder for file {file}", sourceFilePath);

        var fileName = Path.GetFileName(sourceFilePath);

        var templateFolder = appFolder.Subfolder(fileName);
        logger.LogTrace("Creating template folder {folder}", templateFolder.Path);
        templateFolder.EnsureExists();

        var contentFolder = templateFolder.Subfolder("content");
        logger.LogTrace("Creating template content folder {folder}", contentFolder.Path);
        contentFolder.EnsureExists();

        logger.LogTrace("Copying file {file} to template content folder {folder}", sourceFilePath, contentFolder.Path);
        contentFolder.AcceptCopyOf(sourceFilePath);

        var templateConfig = templateFolder.Subfolder(".template.config");
        logger.LogTrace("Creating template config folder {folder}", templateConfig.Path);
        templateConfig.EnsureExists();

        var templateConfigFile = templateConfig.File("template.json");
        logger.LogTrace("Filling {file} from template prototype and replacing {placeholder} to {replacement}", templateConfigFile.Path, "<NAME>", fileName);
        templateConfigFile.UseContent(TemplatePrototype.Json).UseReplacement("<NAME>", fileName).Save();

        logger.LogInformation("Template folder generated at {folder}", templateFolder.Path);
        return templateFolder;
    }

    async Task InstallTemplate(Folder templateFolder)
    {
        logger.LogDebug("Installing template from folder {folder}", templateFolder.Path);

        var result = await consoleClient.ExecuteAsync("dotnet new install . --force", from: templateFolder.Path);

        if (result.ExitCode != 0)
        {
            logger.LogError("Failed to install template from folder {folder} with exit code {code}", templateFolder.Path, result.ExitCode);
        }
        else
        {
            logger.LogInformation("Template installed from folder {folder}", templateFolder.Path);
        }
    }
}