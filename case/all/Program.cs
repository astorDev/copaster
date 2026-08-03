using Copaster;

return CaseCli.Run("Convert input string to all cases", words =>
{
    var results = CaseConverter.ToAll(words);
    foreach (var result in results)
        Console.WriteLine(result);
}, args);
