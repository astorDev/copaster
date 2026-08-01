ARGS ?= toregistry . --name John

run:
	dotnet run -- $(ARGS)

install:
	dotnet pack -c Release
	dotnet tool install --global --source ./bin/Release Copaster.Copy.Cli --allow-downgrade

install-from-nuget:
	dotnet tool install --global Copaster.Copy.Cli

uninstall:
	dotnet tool uninstall --global Copaster.Copy.Cli

reinstall:
	make uninstall || true
	make install