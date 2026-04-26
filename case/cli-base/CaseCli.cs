using System.CommandLine;

namespace Copaster;

public static class CaseCli
{
    public static int Run(string description, Func<string[], string> convert, string[] args)
    {
        Argument<string> inputArg = new("input") { Description = "The string to convert" };
        RootCommand rootCommand = new(description)
        {
            Arguments = { inputArg }
        };

        rootCommand.SetAction(parseResult =>
        {
            var parsed = CaseConverter.Parse(parseResult.GetValue(inputArg)!);
            var result = convert(parsed);
            Console.WriteLine(result);
            return 0;
        });

        return rootCommand.Parse(args).Invoke();
    }
}
