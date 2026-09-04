using Copaster;

var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddAsRootCommand<MagicCommand>();

using var app = builder.Build("A magic CLI application.");

return app.Run(args);