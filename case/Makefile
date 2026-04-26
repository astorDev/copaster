INPUT ?= InputValue
PROJECT ?= cameled
PROJECTS ?= cameled dotted kebabed pascaled snaked trained upsnaked

run:
	dotnet run --project $(PROJECT) -- $(INPUT)

install:
	dotnet pack $(PROJECT)
	dotnet tool install --global --add-source $(PROJECT)/bin/Release copaster.case.$(PROJECT)

uninstall:
	dotnet tool uninstall --global copaster.case.$(PROJECT)

run-installed:
	$(PROJECT) $(INPUT)

run-all:
	for project in $(PROJECTS); do make run PROJECT=$$project; done

install-all:
	for project in $(PROJECTS); do make install PROJECT=$$project; done

uninstall-all:
	for project in $(PROJECTS); do make uninstall PROJECT=$$project; done

run-installed-all:
	for project in $(PROJECTS); do make run-installed PROJECT=$$project; done