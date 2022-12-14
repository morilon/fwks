using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Fwks.Core.Extensions;
using Fwks.Security.Cryptography.Enums;

namespace Fwks.Security.Cryptography;

public static class Cryptography
{
    private static readonly Encoding _encoding = Encoding.UTF8;

    public static string Encrypt(object value, string encryptionKey, string salt = default)
    {
        try
        {
            if (value == null)
                return string.Empty;

            var plainText = $"{salt}{(value is string ? value.ToString() : value.SerializeObject())}";

            var cryptoJsSalt = "Salted__";
            var byteSalt = new byte[8];

            var rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(byteSalt);

            var (derivedKey, derivedIv) = DeriveKeyAndIvByUsingOpensslEvp(encryptionKey, byteSalt);
            var encryptedBytes = EncryptStringToBytes(plainText, derivedKey, derivedIv);
            var encryptedBytesWithSalt = new byte[byteSalt.Length + encryptedBytes.Length + 8];

            Buffer.BlockCopy(_encoding.GetBytes(cryptoJsSalt), 0, encryptedBytesWithSalt, 0, 8);
            Buffer.BlockCopy(byteSalt, 0, encryptedBytesWithSalt, 8, byteSalt.Length);
            Buffer.BlockCopy(encryptedBytes, 0, encryptedBytesWithSalt, byteSalt.Length + 8, encryptedBytes.Length);

            return Convert.ToBase64String(encryptedBytesWithSalt);
        }
        catch (Exception e)
        {
            throw new CryptographicException("Failed to encrypt value.", e);
        }
    }

    public static string Decrypt(string cipherText, string encryptionKey)
    {
        try
        {
            if (cipherText.IsEmpty())
                return string.Empty;

            var encryptedBytesWithSalt = Convert.FromBase64String(cipherText);
            var salt = new byte[8];
            var encryptedBytes = new byte[encryptedBytesWithSalt.Length - salt.Length - 8];

            Buffer.BlockCopy(encryptedBytesWithSalt, 8, salt, 0, salt.Length);
            Buffer.BlockCopy(encryptedBytesWithSalt, salt.Length + 8, encryptedBytes, 0, encryptedBytes.Length);

            var (key, iv) = DeriveKeyAndIvByUsingOpensslEvp(encryptionKey, salt);

            return DecryptStringFromBytesAes(encryptedBytes, key, iv);
        }
        catch (Exception e)
        {
            throw new CryptographicException("Failed to decrypt value", e);
        }
    }

    public static string Hash(object value, string salt = default, HashType hashType = HashType.Sha256)
    {
        if (value == null)
            return string.Empty;

        var plainText = $"{salt}{(value is string ? value.ToString() : value.SerializeObject())}";

        var hashBytes = new Dictionary<HashType, HashAlgorithm>
        {
            [HashType.Md5] = MD5.Create(),
            [HashType.Sha1] = SHA1.Create(),
            [HashType.Sha256] = SHA256.Create(),
            [HashType.Sha384] = SHA384.Create(),
            [HashType.Sha512] = SHA512.Create()

        }[hashType].ComputeHash(_encoding.GetBytes(plainText));

        return string.Join("", hashBytes.Select(x => x.ToString("X2"))).ToLowerInvariant();
    }

    public static string RandomString(int length = 10)
    {
        var specialChars = "@#$%&!?*-_=".ToCharArray();
        var alphaNumeric = "23456789ABCDEFGHJKMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz".ToCharArray();

        var usableChars = specialChars.Concat(alphaNumeric).ToArray();

        if (length < 1 || length > 256)
            length = 8;

        using var rng = RandomNumberGenerator.Create();

        var random = new byte[length];
        rng.GetNonZeroBytes(random);

        var buffer = new char[length];
        var usableLength = usableChars.Length;

        for (var index = 0; index < length; index++)
        {
            var currentChar = usableChars[random[index] % usableLength];

            if (index == 0 && specialChars.Contains(currentChar))
                currentChar = alphaNumeric[random[index] % usableLength];

            buffer[index] = currentChar;
        }

        rng.Dispose();

        return new string(buffer);
    }

    private static (byte[] key, byte[] iv) DeriveKeyAndIvByUsingOpensslEvp(string encryptionKey, byte[] salt)
    {
        var concatenatedHashes = new List<byte>(48);

        var password = _encoding.GetBytes(encryptionKey);
        var currentHash = Array.Empty<byte>();
        using var md5 = MD5.Create();

        while (concatenatedHashes.Count <= 48)
        {
            var preHash = new byte[currentHash.Length + password.Length + salt.Length];

            Buffer.BlockCopy(currentHash, 0, preHash, 0, currentHash.Length);
            Buffer.BlockCopy(password, 0, preHash, currentHash.Length, password.Length);
            Buffer.BlockCopy(salt, 0, preHash, currentHash.Length + password.Length, salt.Length);

            currentHash = md5.ComputeHash(preHash);
            concatenatedHashes.AddRange(currentHash);
        }

        var key = new byte[32];
        var iv = new byte[16];
        concatenatedHashes.CopyTo(0, key, 0, key.Length);
        concatenatedHashes.CopyTo(key.Length, iv, 0, iv.Length);

        return (key, iv);
    }

    private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        using var memoryStream = new MemoryStream();
        using (var aes = CreateAes(key, iv))
        {
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using var streamWriter = new StreamWriter(cryptoStream);
            streamWriter.Write(plainText);
        }

        return memoryStream.ToArray();
    }

    private static string DecryptStringFromBytesAes(byte[] cipherText, byte[] key, byte[] iv)
    {
        using var memoryStream = new MemoryStream(cipherText);
        using var aes = CreateAes(key, iv);

        var descriptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var cryptoStream = new CryptoStream(memoryStream, descriptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);

        return streamReader.ReadToEnd();
    }

    private static Aes CreateAes(byte[] derivedKey, byte[] derivedIv)
    {
        var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.BlockSize = 128;
        aes.FeedbackSize = 8;
        aes.KeySize = 256;
        aes.IV = derivedIv;
        aes.Key = derivedKey;

        return aes;
    }
}