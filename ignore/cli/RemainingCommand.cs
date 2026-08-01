using Microsoft.Extensions.Logging;

public class RemainingCommand : Command
{
    private readonly Argument<string> pathArgument = new("path")
    {
        Description = "The path from which to list remaining (non-ignored) files.",
        Arity = ArgumentArity.ZeroOrOne,
        DefaultValueFactory = _ => "."
    };

    private readonly Logic logic;

    public RemainingCommand(ILogger<Logic> logger) : base("remaining", "Lists files in a path that are not ignored according to the closest .gitignore.")
    {
        Add(pathArgument);
        SetAction(Execute);

        this.logic = new Logic(logger);
    }

    private void Execute(ParseResult parseResult)
    {
        var path = parseResult.GetRequiredValue(pathArgument);
        var folder = new Folder(path);

        var remaining = logic.GetRemainingFiles(folder);
        
        var filenamesToPrint = remaining.Select(f => Path.GetRelativePath(folder.Path, f.Path));

        filenamesToPrint.ToList().ForEach(Console.WriteLine);
    }

    public class Logic(ILogger<Logic> logger)
    {
        public IReadOnlyCollection<Copaster.File> GetRemainingFiles(Folder folder)
        {
            var rules = LoadGitignoreRules(folder);

            var result = GetRemaining(folder, rules).ToArray();

            logger.LogInformation("Found {Count} remaining files in `{Path}`", result.Length, folder.Path);

            return result;
        }

        private static IEnumerable<Copaster.File> GetRemaining(Folder folder, GitignoreRules rules)
        {
            foreach (var file in folder.ImmediateFiles.Where(f => !rules.IgnoresFile(f.Name)))
            {
                yield return file;
            }

            foreach (var subfolder in folder.Subfolders.Where(f => !rules.IgnoresFolder(f.Name)))
            {
                foreach (var file in GetRemaining(subfolder, rules))
                {
                    yield return file;
                }
            }
        }

        private GitignoreRules LoadGitignoreRules(Folder startingFolder)
        {
            var gitignoreFile = FileLocator.FindUpwards(startingFolder, ".gitignore");
            if (gitignoreFile is null)
            {
                logger.LogInformation("No .gitignore file found starting from `{Path}`", startingFolder.Path);
                return GitignoreRules.Empty;
            }

            logger.LogInformation("Using .gitignore file `{GitignorePath}`", gitignoreFile.Path);
            return GitignoreRules.Parse(gitignoreFile.Lines);
        }
    }
}

