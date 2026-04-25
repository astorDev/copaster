F ?= IO

copy:
	dotnet run -- copy ${F}.cs

install-from-local:
	dotnet pack -c Release
	dotnet tool install --global --source ./bin/Release Copaster.Cli --allow-downgrade

install:
	dotnet tool install --global Copaster.Cli

uninstall:
	dotnet tool uninstall --global Copaster.Cli

reinstall:
	make uninstall || true
	make install-from-local