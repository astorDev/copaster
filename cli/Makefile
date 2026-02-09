F ?= IO

copy:
	dotnet run -- copy ${F}.cs

install:
	dotnet pack -c Release
	dotnet tool install --global --source ./bin/Release Copaster.Cli --allow-downgrade

uninstall:
	dotnet tool uninstall --global Copaster.Cli

reinstall:
	make uninstall || true
	make install