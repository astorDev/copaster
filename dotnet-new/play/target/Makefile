X ?= singlelineconsole
IN ?= .
F ?= SingleLineConsole.cs
ALLOW-SCRIPTS ?= Yes

scaffold:
	dotnet new ${X} --output ${IN} --allow-scripts ${ALLOW-SCRIPTS}

scaffold-simple:
	dotnet new ${X} --output ${IN}

delete:
	find . -name "${F}" -delete

again:
	make delete
	make scaffold

format:
	dotnet format Copaster.DotnetNew.Playground.Target.csproj --severity info --verbosity diagnostic --exclude-diagnostics IDE0130

format-with-ns:
	dotnet format Copaster.DotnetNew.Playground.Target.csproj --severity info --verbosity diagnostic