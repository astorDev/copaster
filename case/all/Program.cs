using Copaster;
using System.CommandLine;

Argument<string> inputArg = new("input") { Description = "The string to convert" };
RootCommand rootCommand = new("Convert input string to all cases")
{
    Arguments = { inputArg }
};

rootCommand.SetAction(parseResult =>
{
    var parsed = CaseConverter.Parse(parseResult.GetValue(inputArg)!);
    foreach (var result in CaseConverter.ToAll(parsed))
        Console.WriteLine(result);
    return 0;
});

return rootCommand.Parse(args).Invoke();
