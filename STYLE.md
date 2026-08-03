# Style Guide

## 1. Don't nest methods; always use variables

Assign intermediate values to variables instead of nesting method calls.

**Bad:**
```csharp
action(CaseConverter.Parse(parseResult.GetValue(inputArg)!));
```

**Good:**
```csharp
var input = parseResult.GetValue(inputArg)!;
var words = CaseConverter.Parse(input);
action(words);
```

---

**Bad:**
```csharp
return Run(description, words => Console.WriteLine(convert(words)), args);
```

**Good:**
```csharp
return Run(description, words =>
{
    var result = convert(words);
    Console.WriteLine(result);
}, args);
```

## 2. Don't create synthetic private methods; use properly formatted lambdas

Use a well-formatted multi-line lambda instead of extracting a private method solely to pass it as a callback.

**Bad:**
```csharp
return CaseCli.Run("Convert input string to all cases", Print, args);

void Print(string[] words)
{
    var results = CaseConverter.ToAll(words);
    foreach (var result in results)
        Console.WriteLine(result);
}
```

**Good:**
```csharp
return CaseCli.Run("Convert input string to all cases", words =>
{
    var results = CaseConverter.ToAll(words);
    foreach (var result in results)
        Console.WriteLine(result);
}, args);
```
