uninstall-fix-ns:
	make -C namespaces/cli uninstall

install-fix-ns:
	make -C namespaces/cli install

cli-install:
	make -C cli reinstall

copy-reactivity:
	make -C dotnet-new/play install

paste-reactivity:
	make -C dotnet-new/play/target scaffold

copy-io:
	make -C cli copy-io

paste-io:
	make -C dotnet-new/play/target scaffold X=IO.cs

copaste-io:
	make copy-io
	make paste-io
	code dotnet-new/play/target/buffer/IO.cs

paste-app-folder:
	make -C dotnet-new/play/target scaffold X=AppFolder.cs

repaste-reactivity:
	make clean
	make -C dotnet-new/play/target scaffold

clean:
	make -C dotnet-new/play/target clean

round:
	make copy-reactivity
	make repaste-reactivity

fresh-buffer:
	make remove-buffer
	make create-buffer

remove-buffer:
	dotnet sln remove buffer
	rm -rf buffer

create-buffer:
	dotnet new console --name Copaster.Buffer --output buffer
	dotnet sln add buffer

full-copaster-base:
	make -C cli reinstall
	make buf
	make copy-copaster-base
	make paste-copaster-base

copy-copaster-base:
	copaster copy cli/IO.cs,cli/TemplatePrototype.cs --name CopasterBase

paste-copaster-base:
	copaster paste CopasterBase --to buffer