# Copaster

Copy-Paste Distribution Tool. Copy paste distribution is becoming increasingly popular, especially in Front-end world with tools like shadcn. This project aims to provide a common foundation for such distribution. Currently tool is tailored for .NET development and based heavily on other .NET tooling:

1. Copy a file(s) or folder from a library as a smart-template to central registry on your PC:

```sh
copaster copy some-lib/Reactivity.cs
```

2. Paste the file in any of your project (`cd app-one`):

```sh
copaster paste Reactivity.cs
```

Now, instead of black-box dependency in your project you have module, that you own. A module that you can freely debug, analyze and customize.

## Install & Play Around

Start by installing it globally using .NET CLI:

```sh
dotnet tool install --global Copaster.Cli
```

Now, you should be able to see the list of commands by running from anywhere on your computer:

```sh
copaster --help
```

Now you are fully set to start using it on your own libraries and projects!

## Playing Around

If you want to test the tool with a working example you can use this repository. The [play](/play/) folder was made specifically for this (and [buffer](/buffer/) folder is gitignored for the same reason). Let's clone the repo and get started from it:

```sh
git clone https://github.com/astorDev/copaster.git && cd copaster
```

Let's copy an example file:

```sh
copaster copy play/Reactivity.cs
```

Now, let's create an example project in the buffer, just to see the whole picture

```sh
dotnet new classlib -o buffer --name Copaster.Buffer 
```

With that in place we should be able to `cd buffer` and paste the Reactivity.cs directly to it's root:

```sh
copaster paste Reactivity.cs
```

Note that the namespace was also `Copaster.Buffer` . This is a "smart" part of the template - it runs `fix-ns` util on the project automatically after copying. The `fix-ns` util is part of the project, you can find it [in this folder](/namespaces/cli/)

## How it works?

Copaster is based on `dotnet new` templating functionality. Every copaster template is essentially a dotnet new template, stored in the application's folder. 
