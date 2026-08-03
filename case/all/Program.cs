using Copaster;

return CaseCli.Run(
    "Convert input string to all cases",
    words => { foreach (var result in CaseConverter.ToAll(words)) Console.WriteLine(result); },
    args);
