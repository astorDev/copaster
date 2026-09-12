namespace Copaster;

public class MagicFolderCommand : Command
{
    private readonly string outputFolder;
    private readonly string callerFolder;
    private readonly Magicfile magicfile;
    private readonly RuleRunner ruleRunner;
    private readonly VariablesContext variablesContext;

    public MagicFolderCommand(string outputFolder, string callerFolder, Magicfile magicfile, RuleRunner ruleRunner) 
        : base(outputFolder, $"Executes {Magicfile.InCallerRuleName} and {Magicfile.InOutputRuleName} rules from {Magicfile.Filename} in the {outputFolder} folder.")
    {
        this.outputFolder = outputFolder;
        this.callerFolder = callerFolder;
        this.magicfile = magicfile;
        this.ruleRunner = ruleRunner;

        var varUseParams = VarUseCommandParams.From(magicfile.Placeholders);
        this.variablesContext = new VariablesContext(varUseParams, this.magicfile.Assignments);
        varUseParams.AddTo(this);
        
        SetAction(Execute);
    }

    public async Task Execute(ParseResult parseResult)
    {
        var variables = variablesContext.GetFinalVariables(parseResult);

        await magicfile.ExecuteInOutputRuleIfDefined(ruleRunner, outputFolder, variables);
        await magicfile.ExecuteInCallerRuleIfDefined(ruleRunner, callerFolder, variables);
    }
}