# `copaster-magic` Utility

The util accepts single positional argument with relative folder name. For example:

```sh
copaster-magic example <args for copaster.Makefile in the example folder>
```

Searches `copaster.Makefile` in the passed folder. Runs rules:

- `in-output` in the passed folder e.g. `example`
- `in-caller` in the folder from which the tool was called i.e. `.`

The util uses [Tell.Makefile](https://github.com/astorDev/tell/tree/main/makefile)

## Help

```sh
copaster-magic example --help
```

Explains which arguments from the `copaster.Makefile` are present