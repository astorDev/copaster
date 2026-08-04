install:
	dotnet pack cli
	dotnet tool install --global --add-source cli/bin/Release Copaster.Replace.Cli

uninstall:
	dotnet tool uninstall --global Copaster.Replace.Cli

forth:
	dotnet run --project cli-2 -- in OriginalSymbols ReplacedSymbols --folder example --all-cases 

back:
	dotnet run --project cli-2 -- in --folder example --all-cases ReplacedSymbols OriginalSymbols