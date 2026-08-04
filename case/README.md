# Copaster.Case

Set of tools for converting an input string in any format to target format. Try it out without installing with recipes from makefile:

```sh
make run PROJECT=cameled
make run PROJECT=dotted
make run PROJECT=kebabed
make run PROJECT=pascaled
make run PROJECT=snaked
make run PROJECT=trained
make run PROJECT=upsnaked
make run PROJECT=all
```

After installing utilities as a global tool, you should be able to use the util like this:

```sh
cameled "input-value" # returns: inputValue
pascaled "input-value" # InputValue
kebabed "InputValue" # input-value
# ... and so on for each case 
all-cases "input-value" # returns all cases
```

## Project Structure

- lib: Contains underlying logic for all tools. Parses input string to a universal "case-independent" representation. Provides method to go from the universal representation to a specific casing (listed below)
- pascaled: Minimal console util for conversion to PascalCase
- cameled: Minimal console util for conversion to camelCase
- kebabed: Minimal console util for conversion to kebab-case
- snaked: Minimal console util for conversion to snake_case
- upsnaked: Minimal console util for conversion to UPPER_SNAKE_CASE
- trained: Minimal console util for converstion to Train-Case
- dotted: Minimal console util for conversion to dot.case
- all: Minimal console util (`all-cases`) outputting the input string in all cases