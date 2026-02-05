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