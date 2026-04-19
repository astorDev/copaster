namespace Copaster;

public record FolderIgnore(string FolderName)
{
    public static FolderIgnore? Parse(string line)
    {
        if (line.EndsWith('/')) return new FolderIgnore(line.TrimEnd('/'));

        return null;
    }

    public bool Ignored(Copaster.Folder folder)
    {
        var segments = folder.Path.Split(Path.DirectorySeparatorChar);
        return segments.Contains(FolderName);
    }
}

public record FilenameIgnore(string CorePattern, bool AllowAnyPrefix, bool AllowAnySuffix)
{
    public static FilenameIgnore? Parse(string line)
    {
        var corePattern = line.Trim('*');
        var allowAnyPrefix = line.StartsWith('*');
        var allowAnySuffix = line.EndsWith('*');  

        return new FilenameIgnore(corePattern, allowAnyPrefix, allowAnySuffix);
    }

    public bool IsIgnored(File file)
    {
        var name = file.Name;
        var contains = name.Contains(CorePattern);

        if (AllowAnyPrefix && AllowAnySuffix) return contains;
        if (AllowAnyPrefix) return name.EndsWith(CorePattern);
        if (AllowAnySuffix) return name.StartsWith(CorePattern);

        return name == CorePattern;
    }
}

public class IgnoreCollection(FolderIgnore[] folderIgnore, FilenameIgnore[] filenameIgnore)
{
    public static IgnoreCollection From(IEnumerable<string>? rules)
    {
        var folderIgnores = new List<FolderIgnore>();
        var filenameIgnores = new List<FilenameIgnore>();

        foreach (var rule in rules ?? Array.Empty<string>())
        {
            var folderIgnore = FolderIgnore.Parse(rule);
            if (folderIgnore is not null)
            {
                folderIgnores.Add(folderIgnore);
                continue;
            }
            var filenameIgnore = FilenameIgnore.Parse(rule);
            if (filenameIgnore is not null)
            {
                filenameIgnores.Add(filenameIgnore);
                continue;
            }
            throw new($"Unsupported ignore rule: {rule}");
        }

        return new IgnoreCollection(folderIgnores.ToArray(), filenameIgnores.ToArray());
    }

    public bool IsIgnored(File file)
    {
        return filenameIgnore.Any(r => r.IsIgnored(file));
    }

    public bool IsIgnored(Folder folder)
    {
        return folderIgnore.Any(r => r.Ignored(folder));
    }
}