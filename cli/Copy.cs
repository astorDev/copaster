using System.CommandLine;
using Microsoft.Extensions.Logging;

namespace Copaster;

public class CopyCommand : Command
{
    static readonly Argument<string> SourceFileArgument = new(
        name: "source"
    )
    {
        Description = "The file(s) to copy as a template. For multiple files, separate paths with a comma."
    };

    static readonly Option<string> TemplateNameOption = new(
        name: "--name",
        aliases: ["-n"]
    )
    {
        Description = "The name of the template to be created. Required when copying multiple files."
    };

    private readonly ILogger<CopyCommand> logger;
    private readonly AppFolder appFolder;
    private readonly ConsoleClient consoleClient;

    public CopyCommand(
        AppFolder appFolder, 
        ILogger<CopyCommand> logger,
        ConsoleClient consoleClient) : base("copy", "Copy the specified file(s) as a smart template to the central registry")
    {
        this.logger = logger;
        this.appFolder = appFolder;
        this.consoleClient = consoleClient;
    
        Add(SourceFileArgument);
        Add(TemplateNameOption);

        SetAction(async (parsed) =>
        {
            var sourceRaw = parsed.GetRequiredValue(SourceFileArgument);
            var source = CopySource.Parse(sourceRaw);
            var templateName = parsed.GetValue(TemplateNameOption);

            var templateFolder = GenerateTemplateFolder(source, templateName);

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

    Folder GenerateTemplateFolder(CopySource source, string? templateName)
    {
        logger.LogDebug("Generating template folder for file {file}, with passed template name: {templateName}", source, templateName);

        templateName = source.GetTemplateName(templateName);

        var templateFolder = appFolder.Subfolder(templateName);
        logger.LogTrace("Creating template folder {folder}", templateFolder.Path);
        templateFolder.EnsureExists();

        var contentFolder = templateFolder.Subfolder("content");
        logger.LogTrace("Creating template content folder {folder}", contentFolder.Path);
        contentFolder.EnsureExists();

        foreach (var file in source.Files)
        {
            logger.LogTrace("Copying file {file} to template content folder {folder}", file.Path, contentFolder.Path);
            contentFolder.AcceptCopyOf(file);
        }

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

public record CopySource(File[] Files)
{
    public static CopySource Parse(string input)
    {
        var files = input.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(path => new File(path.Trim()))
            .ToArray();

        return new CopySource(files);
    }

    public string GetTemplateName(string? name)
    {
        if (name != null) return name;
        if (Files.Length == 1) return Files[0].Name;

        throw new("Name must be provided when copying multiple files");
    }
}