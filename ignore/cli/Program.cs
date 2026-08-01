var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddCommand<RemainingCommand>();

using var app = builder.Build("A copaster.ignore CLI application.");

return app.Run(args);