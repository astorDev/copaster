F ?= Reactivity.cs

full:
	make -C source reinstall
	make -C target again

template:
	mkdir -p buffer/${F}/content
	mkdir -p buffer/${F}/.template.config
	cp source/${F} buffer/${F}/content/${F}
	cp template.json buffer/${F}/.template.config/template.json
	cp source/Makefile buffer/${F}/Makefile
	sed -i '' 's/<NAME>/${F}/g' buffer/${F}/.template.config/template.json

file-forward:
	make template
	make -C buffer/${F} install
	make -C target scaffold-simple X=${F}

file-full:
	make file-clean
	make file-forward

file-clean:
	make -C buffer/${F} uninstall
	make remove-buffer
	make remove-scaffolded

remove-scaffolded:
	rm -f target/${F}

remove-buffer:
	rm -rf buffer

remove-template:
	rm -rf buffer/${F}

retemplate:
	make remove-template
	make template