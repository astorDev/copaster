X ?= singlelineconsole
IN ?= .
F ?= SingleLineConsole.cs
ALLOW-SCRIPTS ?= Yes

scaffold:
	dotnet new ${X} --output ${IN} --allow-scripts ${ALLOW-SCRIPTS}

delete:
	find . -name "${F}" -delete

again:
	make delete
	make scaffold