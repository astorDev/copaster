The utility should provide the following capabilities:

1. Save a single C# file for distribution without a need for any configuration file.
2. Allow to automatically change `namespace` in the file to either: `ProjectRootNamespace` or `ProjectRootNamespace` + `{folder}`.
    - The way the namespace is used can be defined by global configuration, project (folder) configuration, command line argument