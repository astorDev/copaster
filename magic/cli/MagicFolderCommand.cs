namespace Copaster;

public class MagicFolderCommand : Command
{
    private readonly string outputFolder;
    private readonly string callerFolder;
    private readonly Magicfile magicfile;
    private readonly RuleRunner ruleRunner;
    private readonly VarUseCommandParams varUseParams;

    public MagicFolderCommand(string outputFolder, string callerFolder, Magicfile magicfile, RuleRunner ruleRunner) 
        : base(outputFolder, $"Executes {Magicfile.InCallerRuleName} and {Magicfile.InOutputRuleName} rules from {Magicfile.Filename} in the {outputFolder} folder.")
    {
        this.outputFolder = outputFolder;
        this.callerFolder = callerFolder;
        this.magicfile = magicfile;
        this.ruleRunner = ruleRunner;

        this.varUseParams = VarUseCommandParams.From(magicfile.Placeholders);
        this.varUseParams.AddTo(this);
        
        SetAction(Execute);
    }

    private async Task Execute(ParseResult parseResult)
    {
        var variables = varUseParams.GetVarValues(parseResult);
        variables = magicfile.Assignments.TransformVariables(variables);

        await magicfile.ExecuteInOutputRuleIfDefined(ruleRunner, outputFolder, variables);
        await magicfile.ExecuteInCallerRuleIfDefined(ruleRunner, callerFolder, variables);
    }
}