var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddAsRootCommand<PasteCommand>();

using var app = builder.Build("A copaster.paste CLI application.");

return app.Run(args);