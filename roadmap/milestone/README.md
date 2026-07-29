# Copaster Current Milestone: V1

- [ ] Copy util, saving files to global registry. [Under Design](../../copy/README.md)
- [ ] Config module, making transformations setup possible. [Under Design](../../config/README.md)
- [ ] Paste util, handling smart insertion of files from the global registry. [Under Design](../../paste/README.md)
- [ ] Helper Utilities and modules, making "smart" copying possible
  - [ ] Ignore util, providing a sanitized list of files for the copy util. [Under Design](../../ignore/README.md)
  - [x] Case util, allowing transforming a string in any source casing to any feasible target casing
  - [x] Replace util, replacing literal strings, including in folder and file names

## Flow

- `copy` requests a sanitized list of files to copy from the `ignore` util.
- `copy` saves those files one-by-one in the global registry, including `config`-related files.
- `paste` copies files from the global registry to the target folder.
- `paste` applies transformations to the inserted files using `case` and `replace` utils, based on `config`.