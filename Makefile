uninstall-fix-ns:
	make -C namespaces/cli uninstall

install-fix-ns:
	make -C namespaces/cli install

copy-reactivity:
	make -C dotnet-new/play install

paste-reactivity:
	make -C dotnet-new/play/target scaffold

repaste-reactivity:
	make clean
	make -C dotnet-new/play/target scaffold

clean:
	make -C dotnet-new/play/target clean

round:
	make copy-reactivity
	make repaste-reactivity