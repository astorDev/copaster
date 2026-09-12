using Copaster;
using Microsoft.Extensions.DependencyInjection;

var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddAsRootCommand<MagicCommand>();
builder.Services.AddSingleton<MagicGate>();

using var app = builder.Build("A magic CLI application.");

var gateResult = app.Services.GetRequiredService<MagicGate>().Process(args);
if (gateResult.FolderPath == null) return gateResult.Action.Invoke();

throw new NotImplementedException("The logic when a folder path is provided is not implemented yet.");