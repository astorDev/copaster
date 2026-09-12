namespace Playground;

[TestClass]
public class FolderAsCommandName
{
    [TestMethod]
    public void WithSlashes()
    {
        var subcommand = new Command("example/here");
        subcommand.SetAction(pr => Console.WriteLine("Hello from example/here"));
        var root = new RootCommand
        {
            subcommand
        };

        var result = root.Parse("example/here").Invoke();
    }

    [TestMethod]
    public void WithBackDots()
    {
        var subcommand = new Command("../../example/here");
        subcommand.SetAction(pr => Console.WriteLine("Hello from ../../example/here"));
        var root = new RootCommand
        {
            subcommand
        };

        var result = root.Parse("../../example/here").Invoke();
    }
}