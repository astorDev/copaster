using System.CommandLine;
using Microsoft.Extensions.Logging;

namespace Copaster;

public class ListCommand : Command
{
    public ListCommand(
        AppFolder appFolder, 
        ILogger<ListCommand> logger) : base("list", "List all templates in the central registry")
    {
        SetAction(async (parsed) =>
        {
            logger.LogDebug("Getting subfolders in {folder}", appFolder.Path);
            appFolder.Subfolders.Select(f => f.Name);

            foreach (var template in appFolder.Subfolders)
            {
                logger.LogInformation(template.Name);
            }
        });
    }
}