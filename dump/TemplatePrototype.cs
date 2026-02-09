namespace Copaster.Dump;

public class TemplatePrototype
{
    public const string Json = """
    {
      "identity": "<NAME>",
      "name": "<NAME>",
      "shortName": "<NAME>",
      "description": "<NAME>",
      "tags": {
        "language": "C#",
        "type": "project"
      },
      "sources": [
        {
          "source": "./content",
          "target": "./"
        }
      ],
      "postActions": [
        {
          "description": "Install fix-ns util",
          "actionId" : "3A7C4B45-1F5D-4A30-959A-51B88E82B5D2", // run script
          "args": {
            "executable": "dotnet",
            "args": "tool install --global Copaster.NamespaceFixer.Cli",
            "redirectStandardOutput": false,
            "redirectStandardError": false
          }
        },
        {
          "description": "Fix Namespaces in Project (and for newly assigned files)",
          "actionId" : "3A7C4B45-1F5D-4A30-959A-51B88E82B5D2", // run script
          "args": {
            "executable": "fix-ns",
            "args": "",
            "redirectStandardOutput": false,
            "redirectStandardError": false
          }
        }
      ]
    }
    """;
}