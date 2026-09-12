global using Tell;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = new CliBuilder();

builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddNiceShell();

builder.Services.AddSingleton<RecipeRunner>();
builder.Services.AddSingleton<RuleRunner>();

builder.AddCliGate<MagicGate>();

using var app = builder.Build("A magic CLI application.", args);

return app.Run(args);