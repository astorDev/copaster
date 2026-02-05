package:
	dotnet pack -c Release -p:PackAsTool=true -p:ToolCommandName=fix-ns

install:
	dotnet tool install --global --add-source ./bin/Release Copaster.NamespaceFixer.Cli

uninstall:
	dotnet tool uninstall --global Copaster.NamespaceFixer.Cli

reinstall:
	make uninstall || true
	make install

refresh:
	make package
	make reinstall

run:
	dotnet run

dirty:
	mkdir -p buffer
	cp ../../Reactivity.cs buffer/Reactivity.cs
	code buffer/Reactivity.cs

play:
	make dirty
	dotnet run
	code buffer/Reactivity.cs