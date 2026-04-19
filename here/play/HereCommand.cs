using System.CommandLine;

namespace Copaster;

public class HereCommand : Command
{
    static readonly Option<string> FromOption = new(
        name: "--from",
        aliases: ["-f"]
    )
    {
        Description = "Path of the folder to copy from",
    };

    static readonly Option<string> ToOption = new(
        name: "--to",
        aliases: ["-t"]
    )
    {
        Description = "Path of the folder to copy to",
    };

    public HereCommand(ILogger<HereCommand> logger) : base("here", "Copies from and to relative folders without any involvement of the global registry")
    {
        this.Add(FromOption);
        this.Add(ToOption);

        this.SetAction((parsed) =>
        {
            var from = parsed.GetValue(FromOption);
            var to = parsed.GetValue(ToOption);

            if (from is null || to is null)
            {
                throw new("Both --name and --to options are required.");
            }

            var fromFolder = new Folder(from);
            var toFolder = new Folder(to);

            logger.LogInformation("Copying from {from} to {to}", from, to);
            
            toFolder.EnsureExists().AcceptCopyOf(fromFolder);
        });
    }
}