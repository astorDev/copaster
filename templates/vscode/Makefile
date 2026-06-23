version := $(shell node -p "require('./package.json').version")
name := $(shell node -p "require('./package.json').name")
publisher := $(shell node -p "require('./package.json').publisher")

install:
	npm install
	npx tsc -p ./
	npx vsce package
	code --install-extension $(name)-$(version).vsix

uninstall:
	code --uninstall-extension $(publisher).$(name)

reinstall: uninstall install
