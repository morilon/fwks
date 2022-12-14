using System.Collections.Generic;
using HashidsNet;

namespace Fwks.Security.Obfuscation;

public static class Obfuscator
{
    private const string DefaultInstance = "__DEFAULT_INSTANCE__";

    private static IDictionary<string, Hashids> _instances;
    private static string _salt;
    private static int _minHashLength;

    public static void Setup(string salt = "__DEFAULT_SALT__", int minHashLength = 7)
    {
        _salt = salt;
        _minHashLength = minHashLength;

        _instances = new Dictionary<string, Hashids>
{
    { DefaultInstance, new Hashids(_salt, _minHashLength) }
};
    }

    public static void Reset()
    {
        _instances = default;
    }

    public static string Encode(int value)
    {
        return _instances[DefaultInstance].Encode(value);
    }

    public static string Encode<T>(int value)
    {
        AddTypedInstance<T>();

        return _instances[typeof(T).Name].Encode(value);
    }

    public static int? Decode(string hash)
    {
        var decoded = _instances[DefaultInstance].Decode(hash);

        return decoded.Length == 0 ? null : decoded[0];
    }

    public static int? Decode<T>(string hash)
    {
        AddTypedInstance<T>();

        var decoded = _instances[typeof(T).Name].Decode(hash);

        return decoded.Length == 0 ? null : decoded[0];
    }

    private static void AddTypedInstance<T>()
    {
        var instanceName = typeof(T).Name;

        if (_instances.ContainsKey(instanceName))
            return;

        _instances.Add(instanceName, new Hashids($"{instanceName}{_salt}", _minHashLength));
    }
}