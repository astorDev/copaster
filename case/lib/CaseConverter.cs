using System.Text.RegularExpressions;

namespace Copaster;

public static class CaseConverter
{
    public static string[] Parse(string input)
    {
        var withSpaces = Regex.Replace(input, @"([a-z])([A-Z])", "$1 $2");
        withSpaces = Regex.Replace(withSpaces, @"([A-Z]+)([A-Z][a-z])", "$1 $2");
        return withSpaces.Split([' ', '-', '_', '.'], StringSplitOptions.RemoveEmptyEntries);
    }

    public static string ToCamel(string[] words) =>
        words[0].ToLower() + string.Concat(words.Skip(1).Select(Capitalize));

    public static string ToPascal(string[] words) =>
        string.Concat(words.Select(Capitalize));

    public static string ToKebab(string[] words) =>
        string.Join("-", words.Select(w => w.ToLower()));

    public static string ToSnake(string[] words) =>
        string.Join("_", words.Select(w => w.ToLower()));

    public static string ToUpperSnake(string[] words) =>
        string.Join("_", words.Select(w => w.ToUpper()));

    public static string ToTrain(string[] words) =>
        string.Join("-", words.Select(Capitalize));

    public static string ToDot(string[] words) =>
        string.Join(".", words.Select(w => w.ToLower()));

    static string Capitalize(string w) =>
        w.Length == 0 ? w : char.ToUpper(w[0]) + w.Substring(1).ToLower();
}
