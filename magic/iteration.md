- [ ] Implement Magic CLI. [Details](#magic-cli)
  - [ ] Add Support for --help without a folder is passed
  - [ ] Add Command with --help based on found copaster.Makefile

## Magic CLI 

```ruby
parsed = MagicGateCommand.parse args
targetFolder = MagicGateCommand.searchTargetFolder parsed
if not targetFolder
    return parsed.InvokeAsync

magicCommand = MagicCommand.from targetFolder
return magicCommand.Run args
```