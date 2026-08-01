public record FolderPattern(string Name)
{
    public bool Matches(string folderName) => folderName == Name;
}

public record FilePattern(string Core, bool AllowAnyPrefix, bool AllowAnySuffix)
{
    public static FilePattern Parse(string pattern)
    {
        var allowAnyPrefix = pattern.StartsWith('*');
        var allowAnySuffix = pattern.EndsWith('*');
        var core = pattern.Trim('*');

        return new FilePattern(core, allowAnyPrefix, allowAnySuffix);
    }

    public bool Matches(string fileName)
    {
        if (AllowAnyPrefix && AllowAnySuffix) return fileName.Contains(Core);
        if (AllowAnyPrefix) return fileName.EndsWith(Core);
        if (AllowAnySuffix) return fileName.StartsWith(Core);

        return fileName == Core;
    }
}

public class GitignoreRules(FolderPattern[] folderPatterns, FilePattern[] filePatterns)
{
    public static GitignoreRules Empty { get; } = new([], []);

    public static GitignoreRules Parse(IEnumerable<string> lines)
    {
        var folderPatterns = new List<FolderPattern>();
        var filePatterns = new List<FilePattern>();

        foreach (var rawLine in lines)
        {
            var line = rawLine.Trim();
            if (line.Length == 0 || line.StartsWith('#')) continue;

            if (line.EndsWith('/'))
            {
                folderPatterns.Add(new FolderPattern(line.TrimEnd('/')));
            }
            else
            {
                filePatterns.Add(FilePattern.Parse(line));
            }
        }

        return new GitignoreRules(folderPatterns.ToArray(), filePatterns.ToArray());
    }

    public bool IgnoresFolder(string folderName) => folderPatterns.Any(p => p.Matches(folderName));

    public bool IgnoresFile(string fileName) => filePatterns.Any(p => p.Matches(fileName));
}
