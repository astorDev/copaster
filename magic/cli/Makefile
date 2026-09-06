ARGS ?= example

run:
	dotnet run -- $(ARGS)

install:
	dotnet pack -c Release
	dotnet tool install --global --source ./bin/Release Copaster.Magic.Cli --allow-downgrade

install-from-nuget:
	dotnet tool install --global Copaster.Magic.Cli

uninstall:
	dotnet tool uninstall --global Copaster.Magic.Cli

reinstall:
	make uninstall || true
	make install