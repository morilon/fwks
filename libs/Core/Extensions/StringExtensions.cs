using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Fwks.Core.Constants;
using Fwks.Core.Exceptions;

namespace Fwks.Core.Extensions;

public static class StringExtensions
{
    public static bool IsEmpty(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool IsEqualTo(this string left, string right, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
    {
        return left.Equals(right, comparisonType);
    }

    public static string RemoveDiacritics(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        text = text.Normalize(NormalizationForm.FormD);

        var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();

        return new string(chars).Normalize(NormalizationForm.FormC);
    }

    public static long TransformBytes(this string format)
    {
        var sections = format.Split(':');

        var isValid = double.TryParse(sections[0], out var value);

        if (sections.Length != 3 || !isValid)
            throw new FormatConversionException(format);

        var from = GetConversionUnit(sections[1]);
        var to = GetConversionUnit(sections[2]);

        return (long)Convert(value, from == 0 ? -to : from);

        static int GetConversionUnit(string value)
        {
            return value.ToLower() switch
            {
                "b" => 0,
                "kb" => 1,
                "mb" => 2,
                "gb" => 3,
                "tb" => 4,
                _ => throw new FormatConversionException(value)
            };
        }

        static double Convert(double value, int unit)
        {
            if (unit == 0)
                return value;

            return value * Math.Pow(1024, unit);
        }
    }

    public static string RemoveSpaces(this string value)
    {
        return value.Replace(" ", string.Empty);
    }

    public static string SpaceTitleCase(this string value)
    {
        return RegexPatterns.TitleCase().Replace(value, " $1").Trim();
    }

    public static string ToPascalCase(this string value, bool removeSpaces = false)
    {
        value = RegexPatterns.PascalCase().Replace(value, match => match.Groups[1].Value.ToUpper());

        return removeSpaces ? value.RemoveSpaces() : value;
    }

    public static string ToCamelCase(this string value, bool removeSpaces = false)
    {
        value = value.ToPascalCase();

        value = value.Length > 0 ? value.Substring(0, 1).ToLower() + value[1..] : value;

        return removeSpaces ? value.RemoveSpaces() : value;
    }

    public static string ToSlugCase(this string value, bool lowerCase = true)
    {
        return ToSlugSnakeCase(value, "-", lowerCase);
    }

    public static string ToSnakeCase(this string value, bool lowerCase = true)
    {
        return ToSlugSnakeCase(value, "_", lowerCase);
    }

    private static string ToSlugSnakeCase(string value, string character, bool toLower)
    {
        value = RegexPatterns.SlugSnakeStepOne().Replace(value, $"$1{character}$2");

        value = RegexPatterns.SlugSnakeStepTwo().Replace(value, $"$1{character}$2");

        value = RegexPatterns.SlugSnakeStepThree().Replace(value, character);

        return toLower ? value.ToLower() : value;
    }
}