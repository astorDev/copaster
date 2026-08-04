var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddCommand<ReplaceInFolderCommand>();

using var app = builder.Build("A copaster-replace CLI application.");

return app.Run(args);