using System.Collections.Immutable;

namespace DotConfig;

internal delegate bool TryParseCallback<T>(string s, out T value);

internal static class Parser
{
    private static readonly Lazy<IReadOnlyDictionary<Type, Delegate>> _dict = new(GetParsers);

    private static IReadOnlyDictionary<Type, Delegate> GetParsers()
    {
        var callback = ImmutableDictionary.CreateBuilder<Type, Delegate>();
        callback[typeof(ulong)] = (TryParseCallback<ulong>)ulong.TryParse;
        callback[typeof(long)] = (TryParseCallback<long>)long.TryParse;
        callback[typeof(uint)] = (TryParseCallback<uint>)uint.TryParse;
        callback[typeof(int)] = (TryParseCallback<int>)int.TryParse;
        callback[typeof(ushort)] = (TryParseCallback<ushort>)ushort.TryParse;
        callback[typeof(short)] = (TryParseCallback<short>)short.TryParse;
        callback[typeof(sbyte)] = (TryParseCallback<sbyte>)sbyte.TryParse;
        callback[typeof(byte)] = (TryParseCallback<byte>)byte.TryParse;
        callback[typeof(float)] = (TryParseCallback<float>)float.TryParse;
        callback[typeof(double)] = (TryParseCallback<double>)double.TryParse;
        callback[typeof(decimal)] = (TryParseCallback<decimal>)decimal.TryParse;
        callback[typeof(bool)] = (TryParseCallback<bool>)bool.TryParse;
        callback[typeof(char)] = (TryParseCallback<char>)char.TryParse;
        callback[typeof(DateTime)] = (TryParseCallback<DateTime>)DateTime.TryParse;
        callback[typeof(TimeSpan)] = (TryParseCallback<TimeSpan>)TimeSpan.TryParse;
        return callback;
    }

    /// <summary>
    ///     Gets the delegate associated with a type, if not found returns nothing.
    /// </summary>
    /// <typeparam name="T">The type to retrieve its value for.</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if no delegate was found.</exception>
    /// <returns></returns>
    public static TryParseCallback<T> Get<T>()
        => (TryParseCallback<T>)_dict.Value[typeof(T)] 
        ?? throw new ArgumentOutOfRangeException(nameof(T), "Type not found, possible NRE if not caught.");

    /// <summary>
    ///     Gets the delegate associated with a type, if not found returns nothing.
    /// </summary>
    /// <param name="type">A Generic type to retrieve the delegate for.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if no delegate was found.</exception>
    /// <returns>A delegate associated with <paramref name="type"/></returns>
    public static Delegate Get(Type type) 
        => _dict.Value[type] 
        ?? throw new ArgumentOutOfRangeException(nameof(type), "Type not found, possible NRE if not caught.");

    public static bool Exists(Type type)
        => _dict.Value.ContainsKey(type);
}
