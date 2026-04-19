namespace Copaster;

public record Folder(string Path)
{
    public string Name => System.IO.Path.GetFileName(Path);
    public File[] ImmediateFiles => [.. Directory.GetFiles(Path).Select(f => new File(f))];

    public IEnumerable<File> AllFiles
    {
        get
        {
            foreach (var file in ImmediateFiles)
            {
                yield return file;
            }
            foreach (var subfolder in Subfolders)
            {
                foreach (var file in subfolder.AllFiles)
                {
                    yield return file;
                }
            }
        }
    }

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

    public File AcceptCopyOf(File sourceFile)
    {
        var destinationPath = System.IO.Path.Combine(Path, sourceFile.Name);
        System.IO.File.Copy(sourceFile.Path, destinationPath, overwrite: true);
        return new File(destinationPath);
    }

    public void AcceptCopyOf(Folder sourceFolder)
    {
        foreach (var file in sourceFolder.ImmediateFiles)
        {
            this.AcceptCopyOf(file);
        }
        
        foreach (var subfolder in sourceFolder.Subfolders)
        {
            var destinationSubfolder = Subfolder(subfolder.Name);
            destinationSubfolder.AcceptCopyOf(subfolder);
        }
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