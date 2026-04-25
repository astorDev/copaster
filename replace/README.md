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

## Coding Guidelines

- Comments are considered Red Flag: If comment explains what a line or block does - that means the . Comments CAN explain WHY, but most of the time instead of explaining the WHY the code can be rewritten in the way, that why is self-explanatory.
- ALWAYS strive to use a package for common functionality, especially for something that is not strictly project-specific