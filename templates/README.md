# Copaster Templates

In the module templates for various project types are stored. 
All templates follow similar base rule:

- They have `Makefile` with commonly used end-to-end commands to perform commonly used action.
- They have `README.md` in the root, with `Getting Started` section describing the most common actions. 
- They try to follow as flat structure as possible, hence put code in the root of the repo.
- They try to make you delete as little code as possible after scaffolding - for that they:
  - For the implementation focus on infrastructure to get version number.
  - Don't have anything that is not strictly necessary or redundant (except `Makefile` and `README` of course)
  - Use the word `Template` extensively and in the way that when replaced by product name it should keep sounding clear.

Currently we have those templates:

- `📁 vscode`: VS Code Extension Template. After `make install` you should be able to see `Starting. Version: {version}` in the extension output window.