//global using Console = NiceShell.Console;

var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddCommand<Copaster.IgnoreCommand>();

using var app = builder.Build("A copaster.ignore CLI application.");

return app.Run(args);