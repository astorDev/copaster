# Copaster Current Milestone: V1

- [ ] Copy util, saving files to global registry. [Under Design](../../copy/README.md) 
- [ ] Config module, making transformations setup possible. [Under Design](../../config/README.md)
- [ ] Paste util, handling smart insertion of the files in a global registry. [Under Design](../../paste/README.md)
- [ ] Helper Utilities and modules, making "smart" copying possible
  - [ ] Ignore util, providing sanitized list of files for copy util to copy. [Under Design](../../ignore/README.md)
  - [x] Case util, allowing transforming a string in any source casing to any feasible target casing
  - [x] Replace util, replacing literal strings, including in folder and file names

## Flow

- `copy` request sanitized list of files to copy from `ignore` util.
- `copy` saves those files one-by-one in the global registry. (including `config`-related)
- `paste` copies files from the global registry to the target folder.
- `paste` applies transformation on the inserted files, using `case` and `replace` utils. (using `config`)