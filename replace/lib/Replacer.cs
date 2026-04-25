namespace Copaster.Replace;

public static class Replacer
{
    public static void ReplaceInFolder(string folderPath, string from, string to)
    {
        ReplaceInFileContents(folderPath, from, to);
        RenameFiles(folderPath, from, to);
        RenameFolders(folderPath, from, to);
    }

    static void ReplaceInFileContents(string folderPath, string from, string to)
    {
        foreach (var file in Directory.EnumerateFiles(folderPath, "*", SearchOption.AllDirectories))
        {
            var content = File.ReadAllText(file);
            if (!content.Contains(from)) continue;
            File.WriteAllText(file, content.Replace(from, to));
        }
    }

    static void RenameFiles(string folderPath, string from, string to)
    {
        foreach (var file in Directory.EnumerateFiles(folderPath, "*", SearchOption.AllDirectories))
        {
            var name = Path.GetFileName(file);
            if (!name.Contains(from)) continue;
            var newPath = Path.Combine(Path.GetDirectoryName(file)!, name.Replace(from, to));
            File.Move(file, newPath);
        }
    }

    static void RenameFolders(string folderPath, string from, string to)
    {
        foreach (var dir in Directory.EnumerateDirectories(folderPath, "*", SearchOption.AllDirectories)
                     .OrderByDescending(d => d.Length))
        {
            var name = Path.GetFileName(dir);
            if (!name.Contains(from)) continue;
            var newPath = Path.Combine(Path.GetDirectoryName(dir)!, name.Replace(from, to));
            Directory.Move(dir, newPath);
        }
    }
}
