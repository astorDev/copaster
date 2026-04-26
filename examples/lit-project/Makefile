ELEMENT ?= CoolTargetElement

target:
	make -C source -f copaster.Makefile

fresh-target:
	make no-target
	make target

no-target:
	rm -rf buffer/target

up:
	make fresh-target
	make -C buffer/target run