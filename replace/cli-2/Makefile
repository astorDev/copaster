ARGS ?= copaster.replaceinfolder . --name John

run:
	dotnet run -- $(ARGS)

install:
	dotnet pack -c Release
	dotnet tool install --global --source ./bin/Release Copaster.ReplaceInFolder.Cli --allow-downgrade

install-from-nuget:
	dotnet tool install --global Copaster.ReplaceInFolder.Cli

uninstall:
	dotnet tool uninstall --global Copaster.ReplaceInFolder.Cli

reinstall:
	make uninstall || true
	make install