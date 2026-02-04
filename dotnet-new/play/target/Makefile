X ?= singlelineconsole
IN ?= .
F ?= SingleLineConsole.cs

scaffold:
	dotnet new ${X} --output ${IN}

delete:
	find . -name "${F}" -delete