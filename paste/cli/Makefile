ARGS ?= . --name John

run:
	dotnet run -- $(ARGS)

install:
	dotnet pack -c Release
	dotnet tool install --global --source ./bin/Release Copaster.Paste.Cli --allow-downgrade

install-from-nuget:
	dotnet tool install --global Copaster.Paste.Cli

uninstall:
	dotnet tool uninstall --global Copaster.Paste.Cli

reinstall:
	make uninstall || true
	make install