using System.CommandLine;
using Copaster.Replace;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Option<string> folderOption = new("--folder")
{
    Description = "The folder to apply replacements in (defaults to current directory)",
    DefaultValueFactory = _ => Directory.GetCurrentDirectory()
};
folderOption.Aliases.Add("-f");

Argument<string> fromArgument = new("from") { Description = "The text to replace" };
Argument<string> toArgument = new("to") { Description = "The replacement text" };

RootCommand rootCommand = new("Replace literal symbols across file contents and file/folder names");
rootCommand.Options.Add(folderOption);
rootCommand.Arguments.Add(fromArgument);
rootCommand.Arguments.Add(toArgument);

rootCommand.SetAction(parseResult =>
{
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddLogging(l => l.AddSimpleConsole(c => c.SingleLine = true));
    var services = serviceCollection.BuildServiceProvider();
    var logger = services.GetRequiredService<ILogger<Program>>();

    var folder = parseResult.GetValue(folderOption)!;
    var from = parseResult.GetValue(fromArgument)!;
    var to = parseResult.GetValue(toArgument)!;

    var resolvedFolder = Path.IsPathRooted(folder) ? folder : Path.Combine(Directory.GetCurrentDirectory(), folder);
    logger.LogInformation("Replacing '{From}' -> '{To}' in {Folder}", from, to, resolvedFolder);
    Replacer.ReplaceInFolder(resolvedFolder, from, to);
    logger.LogInformation("Done");
    return 0;
});

return rootCommand.Parse(args).Invoke();