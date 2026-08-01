# Copaster Copy Util

Copies files and folders to the global buffer/registry. Works in two modes:

### Mode 1: File copy.

When passed a filename (string ending with a file extension) copies the file to the global registry in the folder with the same name or passed `name` argument.

### Mode 2: Folder copy:

1. Finds out which files are supposed to be copied, according to the logic of [Ignore Module](../ignore/README.md).
2. Copies the files into a subfolder of the application folder with either name of source folder or passed `name` argument

### Command

```sh
copaster-copy toregistry [Path=.] [--name]
```

