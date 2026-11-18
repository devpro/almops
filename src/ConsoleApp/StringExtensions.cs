namespace AlmOps.ConsoleApp;

internal static class StringExtensions
{
    /// <summary>
    /// Turns a query such as "url" into the "Url" property name of a model.
    /// </summary>
    public static string FirstCharToUpper(this string input) =>
        input.Length == 0 ? input : $"{char.ToUpperInvariant(input[0])}{input[1..]}";
}
