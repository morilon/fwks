using Fwks.Security.Cryptography.Enums;

namespace Fwks.Security.Cryptography.Extensions;

public static class CryptographyExtensions
{
    public static string ToMd5(this object value, string salt = default)
    {
        return Cryptography.Hash(value, salt, HashType.Md5);
    }

    public static string ToSha1(this object value, string salt = default)
    {
        return Cryptography.Hash(value, salt, HashType.Sha1);
    }

    public static string ToSha256(this object value, string salt = default)
    {
        return Cryptography.Hash(value, salt, HashType.Sha256);
    }

    public static string ToSha384(this object value, string salt = default)
    {
        return Cryptography.Hash(value, salt, HashType.Sha384);
    }

    public static string ToSha512(this object value, string salt = default)
    {
        return Cryptography.Hash(value, salt, HashType.Sha512);
    }

    public static string Encrypt(this object value, string encryptionKey, string salt = default)
    {
        return Cryptography.Encrypt(value, encryptionKey, salt);
    }

    public static string Decrypt(this string value, string encryptionKey)
    {
        return Cryptography.Decrypt(value, encryptionKey);
    }
}