using Microsoft.Extensions.Logging;

namespace Copaster;

public class ConsoleClient(ILogger<ConsoleClient> logger)
{
    public async Task<CliWrap.CommandResult> ExecuteAsync(string fullCommand, string from = null)
    {
        var command = fullCommand.Split(' ')[0];
        var args = fullCommand[command.Length..].Trim();

        logger.LogDebug("Executing command `{command} {args}`", command, args);

        var result = await CliWrap.Cli.Wrap(command)
            .WithArguments(args)
            .WithWorkingDirectory(from ?? Environment.CurrentDirectory)
            .WithStandardOutputPipe(CliWrap.PipeTarget.ToDelegate(e => logger.LogInformation("Command output: {message}", e)))
            .WithStandardErrorPipe(CliWrap.PipeTarget.ToDelegate(e => logger.LogError("Command error: {error}", e)))
            .ExecuteAsync();

        return result;
    }
}