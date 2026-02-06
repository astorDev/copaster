using System.CommandLine;
using Microsoft.Extensions.Logging;

namespace Copaster;

public class PasteCommand : Command
{
    private readonly ILogger<CopyCommand> logger;
    private readonly ConsoleClient consoleClient;

    static readonly Argument<string> PastedArgument = new(
        name: "pasted"
    );

    public readonly Option<string> ToOption = new(
        name: "--to"
    )
    {
        DefaultValueFactory = _ => Environment.CurrentDirectory,
        Description = "The path where to paste the file. If not specified, the file will be pasted to the current directory."
    };

    public PasteCommand(ILogger<CopyCommand> logger, ConsoleClient consoleClient) : base("paste", "Smart-Paste the file")
    {
        this.logger = logger;
        this.consoleClient = consoleClient;

        Add(PastedArgument);
        Add(ToOption);

        SetAction(async (parsed) =>
        {
            var pasted = parsed.GetRequiredValue(PastedArgument);
            var to = parsed.GetRequiredValue(ToOption);
            
            logger.LogDebug("Using dotnet new template to resolve the file {file}", pasted);

            var result = await consoleClient.ExecuteAsync($"dotnet new {pasted} --output {to} --allow-scripts Yes --force");
            if (result.ExitCode != 0)
            {
                logger.LogError("Failed to paste file {file} to {to} with exit code {code}", pasted, to, result.ExitCode);
            }
            else
            {
                logger.LogInformation("File {file} pasted to {to}", pasted, to);
            }
        });
    }
}