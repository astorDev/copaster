public class MagicGate : RootCommand
{
    public static readonly Argument<string> FolderPathArgument = new ("folder-path")
    {
        Description = "The path from which to read copaster.Makefile.",
        Arity = ArgumentArity.ZeroOrOne
    };

    public MagicGate()
    {
        Add(FolderPathArgument);
    }

    public MagicGateResult Process(string[] args)
    {
        var parseResults = Parse(args);
        var folderPath = parseResults.GetValue(FolderPathArgument);

        return new MagicGateResult(folderPath, () => parseResults.Invoke());
    }
}

public record MagicGateResult(string? FolderPath, Func<int> Action);