install:
	dotnet pack cli
	dotnet tool install --global --add-source cli/bin/Release Copaster.Replace.Cli

uninstall:
	dotnet tool uninstall --global Copaster.Replace.Cli

forth:
	dotnet run --project cli-2 -- --folder example --all-cases OriginalSymbols ReplacedSymbols 

back:
	dotnet run --project cli-2 -- --folder example --all-cases ReplacedSymbols OriginalSymbols