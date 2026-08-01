var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddCommand<CopyCommand>();

using var app = builder.Build("A copaster.copy CLI application.");

return app.Run(args);