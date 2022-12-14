namespace Fwks.Security.Obfuscation.Extensions;

public static class ObfuscatorExtensions
{
    public static string Encode(this int value)
    {
        return Obfuscator.Encode(value);
    }

    public static string Encode<T>(this int value)
    {
        return Obfuscator.Encode<T>(value);
    }

    public static int? Decode(this string hash)
    {
        return Obfuscator.Decode(hash);
    }

    public static int? Decode<T>(this string hash)
    {
        return Obfuscator.Decode<T>(hash);
    }
}