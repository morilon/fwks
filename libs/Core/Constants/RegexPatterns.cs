using System.Text.RegularExpressions;

namespace Fwks.Core.Constants;

public static partial class RegexPatterns
{
    [GeneratedRegex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])")]
    public static partial Regex TitleCase();
    [GeneratedRegex("(?:^|_| +)(.)")]
    public static partial Regex PascalCase();
    [GeneratedRegex("([\\p{Lu}]+)([\\p{Lu}][\\p{Ll}])")]
    public static partial Regex SlugSnakeStepOne();
    [GeneratedRegex("([\\p{Ll}\\d])([\\p{Lu}])")]
    public static partial Regex SlugSnakeStepTwo();
    [GeneratedRegex("[-\\s]")]
    public static partial Regex SlugSnakeStepThree();
}