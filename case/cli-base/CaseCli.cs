using System.CommandLine;

namespace Copaster;

public static class CaseCli
{
    public static int Run(string description, Action<string[]> action, string[] args)
    {
        Argument<string> inputArg = new("input") { Description = "The string to convert" };
        RootCommand rootCommand = new(description)
        {
            Arguments = { inputArg }
        };

        rootCommand.SetAction(parseResult =>
        {
            var input = parseResult.GetValue(inputArg)!;
            var words = CaseConverter.Parse(input);
            action(words);
            return 0;
        });

        var parseResult = rootCommand.Parse(args);
        return parseResult.Invoke();
    }

    public static int Run(string description, Func<string[], string> convert, string[] args)
    {
        return Run(description, words =>
        {
            var result = convert(words);
            Console.WriteLine(result);
        }, args);
    }
}
