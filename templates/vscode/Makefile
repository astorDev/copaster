version := $(shell node -p "require('./package.json').version")
name := $(shell node -p "require('./package.json').name")

install:
	npm install
	npx tsc -p ./
	npx vsce package
	code --install-extension $(name)-$(version).vsix

uninstall:
	code --uninstall-extension $(name).$(name)

reinstall: uninstall install
