var cwd = Directory.GetCurrentDirectory();
Console.WriteLine("Hello, I fix namespaces!");
Console.WriteLine($"Current working directory: {cwd}");
Console.WriteLine("Contents:");
foreach (var entry in Directory.EnumerateFileSystemEntries(cwd))
{
    Console.WriteLine(entry);
}