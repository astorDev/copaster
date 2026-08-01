ARGS ?= remaining .

run:
	dotnet run -- $(ARGS)

install:
	dotnet pack -c Release
	dotnet tool install --global --source ./bin/Release Copaster.Ignore.Cli --allow-downgrade

install-from-nuget:
	dotnet tool install --global Copaster.Ignore.Cli

uninstall:
	dotnet tool uninstall --global Copaster.Ignore.Cli

reinstall:
	make uninstall || true
	make install