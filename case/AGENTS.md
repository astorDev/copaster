## Code Style

- No Comments
- No method more then 10 lines
- Use `Microsoft.CommandLine` package for implementing command line utils. Example:

```csharp
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