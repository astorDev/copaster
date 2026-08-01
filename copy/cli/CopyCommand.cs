using Microsoft.Extensions.Logging;

public class CopyCommand : Command
{
    private readonly Option<string> nameOption = new("--name")
    {
        Description = "The name to use when copying a folder. Required for folder copy mode.",
        Required = false
    };

    private readonly Argument<string> pathArgument = new("path")
    {
        Description = "The file or folder path to copy to the registry.",
        Arity = ArgumentArity.ZeroOrOne,
        DefaultValueFactory = _ => "."
    };
    
    private readonly ILogger<CopyCommand> logger;
    private readonly AppFolder appFolder;
    private readonly RemainingCommand.Logic remainingCommandLogic;

    public CopyCommand(
        ILogger<CopyCommand> logger, 
        AppFolder appFolder,
        ILogger<RemainingCommand.Logic> remainingCommandLogicLogger) 
        : base("toregistry", "Copy files and folders to the global buffer/registry")
    {
        Add(pathArgument);
        Add(nameOption);
        SetAction(Execute);

        this.logger = logger;
        this.appFolder = appFolder;
        this.remainingCommandLogic = new RemainingCommand.Logic(remainingCommandLogicLogger);
    }

    private void Execute(ParseResult parseResult)
    {
        var path = parseResult.GetRequiredValue(pathArgument);
        var name = parseResult.GetValue(nameOption);

        logger.LogDebug("Copy requested for path: `{Path}`", path);

        var resolvedPath = Path.GetFullPath(path);

        var sourceFolder = new Folder(resolvedPath);
        var sourceFile = new Copaster.File(resolvedPath);

        if (sourceFile.Exists)
        {
            ExecuteFileCopy(resolvedPath, name ?? sourceFile.Name);
        }
        else if (sourceFolder.Exists)
        {
            ExecuteFolderCopy(sourceFolder, name ?? sourceFolder.Name);
        }
        else
        {
            throw new FileNotFoundException($"The specified path does not exist: {resolvedPath}");
        }
    }

    private void ExecuteFileCopy(string filePath, string name)
    {
        logger.LogInformation("Mode 1: Copying file to registry: `{FilePath}`", filePath);

        var sourceFile = new Copaster.File(filePath);
        var destFolder = appFolder.Subfolder(name).EnsureExists();
        var destFile = destFolder.AcceptCopyOf(sourceFile);

        logger.LogInformation("File copied successfully to: `{DestPath}`", destFile.Path);
    }

    private void ExecuteFolderCopy(Folder sourceFolder, string name)
    {
        logger.LogInformation("Mode 2: Copying folder to registry with name: `{Name}`, from path: `{FolderPath}`", name, sourceFolder.Path);

        var registryFolder = appFolder.Subfolder(name);

        logger.LogDebug("Creating empty registry subfolder at: `{RegistryFolder}`", registryFolder.Path);
        registryFolder.EnsureExists();
        registryFolder.Clean();

        var remainingFiles = remainingCommandLogic.GetRemainingFiles(sourceFolder);
        logger.LogInformation("Found {Count} files to copy", remainingFiles.Count);

        foreach (var file in remainingFiles)
        {
            var relativePath = Path.GetRelativePath(sourceFolder.Path, file.Path);
            var targetSubfolder = registryFolder.Subfolder(Path.GetDirectoryName(relativePath) ?? "");
            
            logger.LogTrace("Copying file: `{FilePath}` to `{TargetFolder}`", file.Path, targetSubfolder.Path);
            targetSubfolder.EnsureExists();
            targetSubfolder.AcceptCopyOf(file);
        }

        logger.LogInformation("Folder copied successfully to: `{RegistryFolder}`", registryFolder.Path);
    }
}