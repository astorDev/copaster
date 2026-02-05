using System.Text.RegularExpressions;
using System.Xml.Linq;

var cwd = Directory.GetCurrentDirectory();
Console.WriteLine("Hello, I fix namespaces!");

Console.WriteLine("args: " + String.Join(", ", args));
Console.WriteLine($"Current working directory: {cwd}");

var csprojPath = FindClosestCsproj(cwd);
if (csprojPath is null)
{
    Console.WriteLine("Error: No .csproj file found in current or parent directories.");
    return;
}

var projectNamespace = new CSProjAnalyzer(csprojPath).GetRootNamespace();
Console.WriteLine($"Project root namespace: {projectNamespace}");
Console.WriteLine("Files to fix: ");

var fixes = new CSFolder(cwd)
    .GetAllFiles(f => !f.EndsWith("Program.cs"))
    .Select(f => NamespaceFix.IfNeeded(f, projectNamespace))
    .Where(fix => fix is not null)
    .Select(fix => fix!)
    .ToList();

foreach (var fix in fixes!)
{
    Console.WriteLine($"{fix.File.FilePath}: Current Namespace: {fix.File.GetNamespace()}. Project Namespace: {projectNamespace}");
}

if (args.Contains("--dry-run") || fixes.Count == 0)
{
    Console.WriteLine("Dry run complete. No changes made.");
    return;
}

fixes.ForEach(fix => fix.Apply());
Console.WriteLine("Namespace fixes applied.");

static string? FindClosestCsproj(string startPath)
{
    var current = new DirectoryInfo(startPath);
    while (current is not null)
    {
        var csproj = Directory.EnumerateFiles(current.FullName, "*.csproj").FirstOrDefault();
        if (csproj is not null) return csproj;
        current = current.Parent;
    }
    return null;
}

public class CSProjAnalyzer(string csprojPath)
{
    public string GetRootNamespace()
    {
        var doc = XDocument.Load(csprojPath);
        var rootNamespace = doc.Root?.Descendants("RootNamespace").FirstOrDefault()?.Value;
        return rootNamespace
            ?? Path.GetFileNameWithoutExtension(csprojPath);
    }
}

public record NamespaceFix(CSFile File, string TargetNamespace)
{
    public static NamespaceFix? IfNeeded(CSFile csFile, string targetNamespace)
    {
        var currentNamespace = csFile.GetNamespace();
        if (currentNamespace == targetNamespace)
            return null;
        return new NamespaceFix(csFile, targetNamespace);
    }

    public void Apply()
    {
        File.ReplaceNamespace(TargetNamespace);
    }
}

public class CSFolder
{
    private readonly string _folderPath;

    public CSFolder(string folderPath)
    {
        _folderPath = folderPath;
    }

    public IEnumerable<CSFile> GetAllFiles(Func<string, bool>? filter = null)
    {
        foreach (var filePath in Directory.EnumerateFiles(_folderPath, "*.cs", SearchOption.AllDirectories))
            if (!filePath.Contains("/bin/") && !filePath.Contains("/obj/"))
                if (filter is null || filter(filePath))
                    yield return new CSFile(filePath);
    }
}

public class CSFile
{
    private readonly string _content;
    public readonly string FilePath;

    private string? _namespace;
    public string Namespace => _namespace ??= GetNamespace();

    public CSFile(string filePath)
    {
        FilePath = filePath;
        _content = File.ReadAllText(filePath);
    }

    public string GetNamespace()
    {
        var match = Regex.Match(_content, @"namespace\s+([\w\.]+)");
        return match.Success ? match.Groups[1].Value : "";
    }

    internal void ReplaceNamespace(string targetNamespace)
    {
        var updatedContent = Regex.Replace(_content, @"namespace\s+[\w\.]+", $"namespace {targetNamespace}");
        File.WriteAllText(FilePath, updatedContent);
    }
}