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
            action(CaseConverter.Parse(parseResult.GetValue(inputArg)!));
            return 0;
        });

        return rootCommand.Parse(args).Invoke();
    }

    public static int Run(string description, Func<string[], string> convert, string[] args) =>
        Run(description, words => Console.WriteLine(convert(words)), args);
}
