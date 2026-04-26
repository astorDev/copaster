## Code Style

- No Comments
- No method more then 10 lines
- No double nesting (no if inside a loop for example). Extract in dedicated method instead.
- Use `System.CommandLine` package for implementing command line utils, following this example:

```csharp
// using System.CommandLine

Option<FileInfo> fileOption = new("--file")
{
    Description = "The file to read and display on the console"
};

RootCommand rootCommand = new("Sample app for System.CommandLine");
rootCommand.Options.Add(fileOption);

rootCommand.SetAction(parseResult =>
{
    FileInfo parsedFile = parseResult.GetValue(fileOption);
    ReadFile(parsedFile);
    return 0;
});

ParseResult parseResult = rootCommand.Parse(args);
return parseResult.Invoke();
```