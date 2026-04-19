namespace Copaster;

public record File(string Path)
{
    public string? _content;
    public string Content
    {
        get => _content ??= System.IO.File.ReadAllText(Path);
    }

    public string Name => System.IO.Path.GetFileName(Path);

    public File UseContent(string content)
    {
        _content = content;
        return this;
    }

    public File UseReplacement(string placeholder, string replacement)
    {
        _content = Content.Replace(placeholder, replacement);
        return this;
    }

    public void Replace(string placeholder, string replacement)
    {
        UseReplacement(placeholder, replacement).Save();
    }

    public File Save()
    {
        System.IO.File.WriteAllText(Path, _content);
        return this;
    }

    public static void Replace(string filePath, string placeholder, string replacement)
    {
        new File(filePath).UseReplacement(placeholder, replacement).Save();
    }
}