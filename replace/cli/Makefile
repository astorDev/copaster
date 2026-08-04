ARGS ?= in . --help

run:
	dotnet run -- $(ARGS)

install:
	dotnet pack -c Release
	dotnet tool install --global --source ./bin/Release Copaster.Replace.Cli --allow-downgrade

install-from-nuget:
	dotnet tool install --global Copaster.Replace.Cli

uninstall:
	dotnet tool uninstall --global Copaster.Replace.Cli

reinstall:
	make uninstall || true
	make install