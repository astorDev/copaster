# Copaster Ignore

Some files are not supposed to be copied from a template (e.g., `bin/`, `obj/`, `node_modules/`, etc.) — think gitignored files. This util provides a sanitized (post-ignore) list of files in a folder so other utils know what to copy.

The util gets ignore rules from `.gitignore` files. It first searches for the file in the root of the specified directory. If not found, it goes one directory up and searches there. Once a `.gitignore` is found, the util stops searching and uses that file.

## Commands

```sh
copaster-ignore remaining [Path=.]
```

Returns files in the specified path, including all subfolders, except for the ignored ones. 