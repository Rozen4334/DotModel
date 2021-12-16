using System.Collections.Immutable;

namespace DotModel.Serialization;

internal static class TypeLookup
{
    private static readonly Lazy<IReadOnlyDictionary<string, Type>> _dict = new(GetLookups);

    private static IReadOnlyDictionary<string, Type> GetLookups()
    {
        var callback = ImmutableDictionary.CreateBuilder<string, Type>();
        callback["string"] = typeof(string);
        callback["boolean"] = typeof(bool);
        callback["long"] = typeof(long);
        callback["short"] = typeof(short);
        callback["int"] = typeof(int);
        callback["ulong"] = typeof(ulong);
        callback["ushort"] = typeof(ushort);
        callback["uint"] = typeof(uint);
        callback["datetime"] = typeof(DateTime);
        callback["timespan"] = typeof(TimeSpan);
        callback["float"] = typeof(float);
        callback["double"] = typeof(double);
        callback["decimal"] = typeof(decimal);
        callback["byte"] = typeof(byte);
        callback["sbyte"] = typeof(sbyte);
        return callback;
    }

    public static Type Get(string type)
        => _dict.Value[type]
        ?? throw new ArgumentOutOfRangeException(nameof(type), "Type not found, possible NRE if not caught.");

    public static bool Exists(string type)
        => _dict.Value.ContainsKey(type);
}

