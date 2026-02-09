using System.CommandLine;
using Microsoft.Extensions.Logging;

namespace Copaster;

public class CopyCommand : Command
{
    static readonly Argument<string> SourceFileArgument = new(
        name: "source"
    );

    static readonly Option<string> TemplateNameOption = new(
        name: "--name",
        aliases: ["-n"]
    )
    {
        Description = "The name of the template to be created. If not specified, the file name will be used."
    };

    private readonly ILogger<CopyCommand> logger;
    private readonly AppFolder appFolder;
    private readonly ConsoleClient consoleClient;

    public CopyCommand(
        AppFolder appFolder, 
        ILogger<CopyCommand> logger,
        ConsoleClient consoleClient) : base("copy", "Copy the specified file as a smart template to the central registry")
    {
        this.logger = logger;
        this.appFolder = appFolder;
        this.consoleClient = consoleClient;
    
        Add(SourceFileArgument);
        Add(TemplateNameOption);

        SetAction(async (parsed) =>
        {
            var sourceFilePath = parsed.GetRequiredValue(SourceFileArgument);
            var templateName = parsed.GetValue(TemplateNameOption);

            var templateFolder = GenerateTemplateFolder(sourceFilePath, templateName);

            try
            {
                await InstallTemplate(templateFolder);
            }
            catch (Exception)
            {
                logger.LogDebug("Cleaning up template folder {folder} due to error during installation", templateFolder.Path);
                templateFolder.Delete();
            }
        });
    }

    Folder GenerateTemplateFolder(string sourceFilePath, string? templateName)
    {
        logger.LogDebug("Generating template folder for file {file}, with passed template name: {templateName}", sourceFilePath, templateName);

        var fileName = Path.GetFileName(sourceFilePath);
        templateName ??= fileName;

        var templateFolder = appFolder.Subfolder(templateName);
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
        logger.LogTrace("Filling {file} from template prototype and replacing {placeholder} to {replacement}", templateConfigFile.Path, "<NAME>", templateName);
        templateConfigFile.UseContent(TemplatePrototype.Json).UseReplacement("<NAME>", templateName).Save();

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