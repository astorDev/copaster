ELEMENT ?= CoolTargetElement

target:
	rsync -av --exclude 'node_modules' source/ buffer/target
	cd buffer/target && ncu -u
	cd buffer/target && npm install
	replace -f buffer/target MyElement $(ELEMENT)
	replace -f buffer/target my-element `kebabed $(ELEMENT)`

fresh-target:
	make no-target
	make target

no-target:
	rm -rf buffer/target

up:
	make fresh-target
	make -C buffer/target run