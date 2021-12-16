using System.Collections.Immutable;

namespace DotModel.Serialization;

internal delegate bool ParseDelegate<T>(string s, out T value);

internal static class TypeParser
{
    private static readonly Lazy<IReadOnlyDictionary<Type, Delegate>> _dict = new(GetParsers);

    private static IReadOnlyDictionary<Type, Delegate> GetParsers()
    {
        var callback = ImmutableDictionary.CreateBuilder<Type, Delegate>();
        callback[typeof(ulong)] = (ParseDelegate<ulong>)ulong.TryParse;
        callback[typeof(long)] = (ParseDelegate<long>)long.TryParse;
        callback[typeof(uint)] = (ParseDelegate<uint>)uint.TryParse;
        callback[typeof(int)] = (ParseDelegate<int>)int.TryParse;
        callback[typeof(ushort)] = (ParseDelegate<ushort>)ushort.TryParse;
        callback[typeof(short)] = (ParseDelegate<short>)short.TryParse;
        callback[typeof(sbyte)] = (ParseDelegate<sbyte>)sbyte.TryParse;
        callback[typeof(byte)] = (ParseDelegate<byte>)byte.TryParse;
        callback[typeof(float)] = (ParseDelegate<float>)float.TryParse;
        callback[typeof(double)] = (ParseDelegate<double>)double.TryParse;
        callback[typeof(decimal)] = (ParseDelegate<decimal>)decimal.TryParse;
        callback[typeof(bool)] = (ParseDelegate<bool>)bool.TryParse;
        callback[typeof(char)] = (ParseDelegate<char>)char.TryParse;
        callback[typeof(DateTime)] = (ParseDelegate<DateTime>)DateTime.TryParse;
        callback[typeof(TimeSpan)] = (ParseDelegate<TimeSpan>)TimeSpan.TryParse;
        return callback;
    }

    /// <summary>
    ///     Gets the delegate associated with a type, if not found returns nothing.
    /// </summary>
    /// <typeparam name="T">The type to retrieve its value for.</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if no delegate was found.</exception>
    /// <returns></returns>
    public static ParseDelegate<T> Get<T>()
        => (ParseDelegate<T>)_dict.Value[typeof(T)] 
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
