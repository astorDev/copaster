namespace Copaster;

public record Magicfile(
    IReadOnlyList<Assignment> Assignments,
    Rule? InOutputRule,
    Rule? InCallerRule
)
{
    public const string Filename = "copaster.Makefile";
    public const string InOutputRuleName = "in-output";
    public const string InCallerRuleName = "in-caller";

    public IEnumerable<Placeholder> Placeholders => new [] { InOutputRule, InCallerRule }.Where(r => r != null).SelectMany(r => r!.Placeholders).Distinct();

    public static Magicfile Load(string folderPath)
    {
        var filePath = Path.Combine(folderPath, Filename);
        var makefile = Makefile.Load(filePath);

        var inOutputRule = makefile.Rules.GetValueOrDefault(InOutputRuleName);
        var inCallerRule = makefile.Rules.GetValueOrDefault(InCallerRuleName);

        return new Magicfile(
            makefile.Assignments,
            inOutputRule,
            inCallerRule
        );
    }

    public async Task ExecuteInOutputRuleIfDefined(RuleRunner runner, string outputFolder, IReadOnlyDictionary<string, string> variables)
    {
        if (InOutputRule == null) return;
        await runner.Run(InOutputRule.Recipes, outputFolder, variables);
    }

    public async Task ExecuteInCallerRuleIfDefined(RuleRunner runner, string callerFolder, IReadOnlyDictionary<string, string> variables)
    {
        if (InCallerRule == null) return;
        await runner.Run(InCallerRule.Recipes, callerFolder, variables);
    }
}