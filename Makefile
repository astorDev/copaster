uninstall-fix-ns:
	make -C namespaces/cli uninstall

install-fix-ns:
	make -C namespaces/cli install

scaffold-reactivity:
	make -C dotnet-new/play/target scaffold

clean:
	make -C dotnet-new/play/target clean