# Replace

Replaces literal symbols of <from> parameter to literal symbols of <to> parameter. The util replaces them accross all files AND most uniquely file and folder names.

Usage:

```sh
replace -f <folder-path | . > <from> <to> 
```

## This Repository

- lib: Contains .NET Library well all the logic is concentrated.
- cli: Contains a runnable .NET Console app
- example: Contains a source folder to which the replace util can be applied for testing.

For simple and curated test of the application use recipes from Makefile in the root of the app.
Use `make forth` command to replace symbols in example folder and inspect results and `make back` to rollback the changes.