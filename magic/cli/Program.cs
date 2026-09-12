global using Tell;
using Copaster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = new CliBuilder();

builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddNiceShell();

builder.Services.AddSingleton<RecipeRunner>();
builder.Services.AddSingleton<RuleRunner>();
builder.Services.AddSingleton<MagicGate>();

using var app = builder.Build("A magic CLI application.");

return app.Run((RuleRunner runner, MagicGate gate) =>
{
    var gateResult = gate.Process(args);
    if (gateResult.FolderPath == null) return gateResult.Action.Invoke();

    var magicfile = Magicfile.Load(gateResult.FolderPath);

    var magicCommand = new RootCommand($"Executes magic in the given folder.")
    {
        new MagicFolderCommand(
            gateResult.FolderPath,
            Directory.GetCurrentDirectory(),
            magicfile,
            runner
        )
    };

    var magicParseResult = magicCommand.Parse(args);
    return magicParseResult.Invoke();
});