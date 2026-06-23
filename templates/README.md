# Copaster Templates

This module contains templates for various project types.
All templates follow a similar set of base rules:

- They have a `Makefile` with end-to-end commands for common actions.
- They have a `README.md` in the root, with a `Getting Started` section describing the most common actions.
- They have `.gitignore` excluding files and folders specific to the project type. 
- They try to follow as flat structure as possible, hence put code in the root of the repo.
- They try to make you delete as little code as possible after scaffolding - for that they:
  - For the implementation focus on infrastructure to get version number.
  - Don't have anything that is not strictly necessary or redundant (except `Makefile`, `.gitignore` and `README` of course)
  - Use the word `Template` extensively and in the way that when replaced by product name it should keep sounding clear.

Currently we have those templates:

- `📁 vscode`: VS Code Extension Template. After `make install` you should be able to see `Template Starting. Version: {version}` in the extension output window.