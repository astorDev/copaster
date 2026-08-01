namespace Copaster;

public static class FileLocator
{
    public static File? FindUpwards(Folder startingFolder, string fileName = ".gitignore")
    {
        var current = startingFolder;

        while (current is not null)
        {
            var candidate = current.File(fileName);
            if (candidate.Exists) return candidate;

            current = current.Parent;
        }

        return null;
    }
}
