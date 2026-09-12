var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddCommand<ExampleCommand>();

using var app = builder.Build("A copaster.magic CLI application.");

return app.Run(args);