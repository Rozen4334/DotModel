namespace DotModel.Serialization;

internal static class SerializerExtensions
{
    public static bool FindParser<T>(T type, out TryParseCallback<T> result) where T : Type
    {
        result = null;
        if (!TypeParser.Exists(type))
            return false;
        else result = TypeParser.Get<T>();
        return true;
    }
}

