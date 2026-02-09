namespace Copaster;

public record Folder(string Path)
{
    public string Name => System.IO.Path.GetFileName(Path);

    public Folder EnsureExists()
    {
        Directory.CreateDirectory(Path);
        return this;
    }

    public Folder Subfolder(string subfolderName)
    {
        var subfolderPath = System.IO.Path.Combine(Path, subfolderName);
        return new Folder(subfolderPath);
    }

    public File AcceptCopyOf(string sourceFilePath)
    {
        var fileName = System.IO.Path.GetFileName(sourceFilePath);
        var destinationPath = System.IO.Path.Combine(Path, fileName);
        System.IO.File.Copy(sourceFilePath, destinationPath, overwrite: true);
        return new File(destinationPath);
    }

    public File File(string fileName)
    {
        var filePath = System.IO.Path.Combine(Path, fileName);
        return new File(filePath);
    }

    public void Delete()
    {
        Directory.Delete(Path, recursive: true);
    }

    public IEnumerable<Folder> Subfolders => Directory.GetDirectories(Path).Select(d => new Folder(d));
}

public record File(string Path)
{
    public string? _content;
    public string Content
    {
        get => _content ??= System.IO.File.ReadAllText(Path);
    }

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