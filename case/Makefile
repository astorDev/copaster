Input ?= InputValue

.PHONY: cameled dotted kebabed pascaled snaked trained upsnaked

cameled:
	dotnet run --project cameled -- $(Input)

dotted:
	dotnet run --project dotted -- $(Input)

kebabed:
	dotnet run --project kebabed -- $(Input)

pascaled:
	dotnet run --project pascaled -- $(Input)

snaked:
	dotnet run --project snaked -- $(Input)

trained:
	dotnet run --project trained -- $(Input)

upsnaked:
	dotnet run --project upsnaked -- $(Input)

all:
	make cameled && make dotted && make kebabed && make pascaled && make snaked && make trained && make upsnaked