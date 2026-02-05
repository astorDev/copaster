## Limitations

- Template `source` can not reference files and folder higher than the `.template.config` folder parent directory
- `dotnet format` doesn't work for fixing namespaces. Moreover IDE0130 even breaks the format command in general. [GH Issue](https://github.com/dotnet/format/issues/1623)