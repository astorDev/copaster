var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddAsRootCommand<ReplaceCommand>();

using var app = builder.Build();

return app.Run(args);