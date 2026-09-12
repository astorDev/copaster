using Copaster;

public class MagicGate : RootCommand, ICliGate
{
    public static readonly Argument<string> FolderPathArgument = new ("folder-path")
    {
        Description = "The path from which to read copaster.Makefile.",
        Arity = ArgumentArity.ZeroOrOne
    };

    private readonly RuleRunner ruleRunner;

    public MagicGate(RuleRunner ruleRunner)
    {
        Add(FolderPathArgument);
        this.ruleRunner = ruleRunner;
    }

    public CliGateResult Process(string[] args)
    {
        var parseResults = Parse(args);
        var folderPath = parseResults.GetValue(FolderPathArgument);

        if (folderPath == null)
        {
            return CliGateResult.Stop(parseResults.Invoke());
        }

        var magicfile = Magicfile.Load(folderPath);
        var command = new MagicFolderCommand(folderPath, Directory.GetCurrentDirectory(), magicfile, ruleRunner);

        return CliGateResult.ContinueWith([ command ]);
    }
}