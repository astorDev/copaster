lib-nuget:
	mkdir -p ./temp
	cp ignore-cli-nuget.yml ./temp/ignore-cli-nuget.yml
	replace --folder ./temp ignore-cli `kebabed $(LIB)`
	replace --folder ./temp Ignore.Cli `pascaled $(LIB)`
	replace --folder ./temp ignore/cli `cameled $(LIB)/lib`
	cp ./temp/* .
	rm -rf ./temp