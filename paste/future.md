- [ ] Combined Paste. [Details](#combined-paste)

## Combined Paste

Allow a template which runs multiple pastes and runs a manipulation based on the output

```Makefile
lib-n-play:
    copaster paste lib $(OUTPUT)/lib
    copaster paste play $(OUTPUT)/play
    dotnet add $(OUTPUT)/play reference $(OUTPUT)/lib
```