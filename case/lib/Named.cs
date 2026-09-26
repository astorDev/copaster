namespace Copaster;

public static class Camel
{
    public static string Of(string input) => CaseConverter.ToCamel(CaseConverter.Parse(input));
}

public static class Pascal
{
    public static string Of(string input) => CaseConverter.ToPascal(CaseConverter.Parse(input));
}

public static class Kebab
{
    public static string Of(string input) => CaseConverter.ToKebab(CaseConverter.Parse(input));
}

public static class Snake
{
    public static string Of(string input) => CaseConverter.ToSnake(CaseConverter.Parse(input));
}

public static class UpperSnake
{
    public static string Of(string input) => CaseConverter.ToUpperSnake(CaseConverter.Parse(input));
}

public static class Train
{
    public static string Of(string input) => CaseConverter.ToTrain(CaseConverter.Parse(input));
}

public static class Dot
{
    public static string Of(string input) => CaseConverter.ToDot(CaseConverter.Parse(input));
}

public static class UpperDot
{
    public static string Of(string input) => CaseConverter.ToUpperDot(CaseConverter.Parse(input));
}

public static class DotPascal
{
    public static string Of(string input) => CaseConverter.ToDotPascal(CaseConverter.Parse(input));
}
    