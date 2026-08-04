using Copaster;
using Copaster.Replace;
using Microsoft.Extensions.Logging;

public class ReplaceInFolderCommand : Command
{
    private readonly Option<string> folderOption = new("--folder")
    {
        Description = "The folder in which to perform replacements. Defaults to the current directory.",
        Required = false,
    };

    private readonly Option<bool> allCasesOption = new("--all-cases")
    {
        Description = "If specified, the replacement will use all possible case pairs.",
        Required = false,
    };

    private readonly Argument<string> fromArgument = new("from")
    {
        Description = "The text to replace.",
        Arity = ArgumentArity.ExactlyOne
    };

    private readonly Argument<string> toArgument = new("to")
    {
        Description = "The replacement text.",
        Arity = ArgumentArity.ExactlyOne
    };

    private readonly ILogger<ReplaceInFolderCommand> logger;

    public ReplaceInFolderCommand(ILogger<ReplaceInFolderCommand> logger) : base("in", "Replaces text and files and folder names in a folder.")
    {
        Add(folderOption);
        Add(allCasesOption);
        Add(fromArgument);
        Add(toArgument);
        SetAction(Execute);

        this.logger = logger;
    }

    private void Execute(ParseResult parseResult)
    {
        var folder = parseResult.GetValue(folderOption) ?? ".";
        var from = parseResult.GetRequiredValue(fromArgument);
        var to = parseResult.GetRequiredValue(toArgument);
        var allCases = parseResult.GetValue(allCasesOption);

        logger.LogDebug("Processing folder {Folder}...", folder);
        var resolvedFolder = Path.IsPathRooted(folder) ? folder : Path.Combine(Directory.GetCurrentDirectory(), folder);

        var replacementPairArray = allCases ? AllCasePairs(from, to) : [ (from, to) ];

        foreach (var (effectiveFrom, effectiveTo) in replacementPairArray)
        {
            logger.LogDebug("Replacing '{From}' -> '{To}' in {Folder}", effectiveFrom, effectiveTo, resolvedFolder);
            Replacer.ReplaceInFolder(resolvedFolder, effectiveFrom, effectiveTo);
        }

        logger.LogInformation("Done");
    }

    public static (string from, string to)[] AllCasePairs(string from, string to)
    {
        var fromWords = CaseConverter.Parse(from);
        var fromInAllCases = CaseConverter.ToAll(fromWords);

        var toWords = CaseConverter.Parse(to);
        var toInAllCases = CaseConverter.ToAll(toWords);

        var result = new (string from, string to)[fromInAllCases.Length];
        for (int i = 0; i < fromInAllCases.Length; i++)
        {
            result[i] = (fromInAllCases[i], toInAllCases[i]);
        }
        return result;
    }
}