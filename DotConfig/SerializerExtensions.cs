namespace DotConfig;

internal static class SerializerExtensions
{
    public static bool FindParser<T>(T type, out TryParseCallback<T> result) where T : Type
    {
        result = default;
        if (!Parser.Exists(type))
            return false;
        else result = Parser.Get<T>();
        return true;
    }
}

