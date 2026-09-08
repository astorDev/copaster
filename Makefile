cli-install:
	make -C cli install

copy-reactivity:
	copaster copy play/Reactivity.cs 

paste-reactivity:
	copaster paste Reactivity.cs --to buffer

copy-a-ending:
	copaster copy play/Letters/A.cs,play/Letters/C/CA.cs --name a-ending

buffer-proj:
	dotnet new classlib -o buffer --name Copaster.Buffer 

no-buffer:
	rm -rf buffer


uninstall-fix-ns:
	make -C namespaces/cli uninstall

install-fix-ns:
	make -C namespaces/cli install

cli-reinstall:
	make -C cli reinstall

cli-uninstall:
	make -C cli uninstall

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
	make cli-reinstall
	make fresh-buffer
	make copy-copaster-base
	make paste-copaster-base

copy-copaster-base:
	copaster copy cli/IO.cs,cli/TemplatePrototype.cs --name CopasterBase

paste-copaster-base:
	copaster paste CopasterBase --to buffer

full-letters:
	make cli-reinstall
	make fresh-buffer
	copaster remove Letters
	copaster copy play/Letters
	copaster paste Letters --to buffer/Letters

feature-branch:
	test "$$(git default-branch)" == "$$(git branch --show-current)" || throw "Current branch is NOT default ($$(git branch --show-current)). Feature branch must be made from the default branch."
	git switch --create $(BRANCH)

full-pr:
	test "$$(git default-branch)" == "$$(git branch --show-current)" || throw "Current branch is NOT default ($$(git branch --show-current)). Full PR is only possible from the default branch."
	git switch --create $(BRANCH)
	git save "$(TITLE)"
	gh pr create --title "$(TITLE)" --body "" || true
	gh pr view --web

pr:
	test "$$(git default-branch)" != "$$(git branch --show-current)" || throw "Current branch is default ($$(git branch --show-current)). This is likely a mistake, PRs should be created from a feature branch."
	git save "$(TITLE)"
	gh pr create --title "$(TITLE)" --body "" || true
	gh pr view --web

post-pr:
	git default-and-burn
	git pull