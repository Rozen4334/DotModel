using System.Collections;

namespace DotModel.Serialization;

internal static class SerializerExtensions
{
    public static bool FindParser<T>(T type, out ParseDelegate<T> result) where T : Type
    {
        result = null;
        if (!TypeParser.Exists(type))
            return false;
        else result = TypeParser.Get<T>();
        return true;
    }

    public static bool FindType(string type, out Type result)
    {
        result = null;
        if (!TypeLookup.Exists(type))
            return false;
        else result = TypeLookup.Get(type);
        return true;
    }

    public static Dictionary<string, Type> FindValuesOfType(List<string> input)
    {
        var result = new Dictionary<string, Type>();

        for(int i = 0; i < input.Count; i++)
        {
            if (!input[i].TrimStart().StartsWith('-'))
                continue;
            var adder = input[i].TrimStart()[2..].Split(' ');

            Type holdingValue;
            if (!FindType(adder[0], out Type type))
            {
                switch (adder[0])
                {
                    case "list":
                        {
                            FindType(adder[0][4..adder[0].Length], out var t);

                            FindParser(t, out var listparse);
                            var selector = 1;
                            for (int e = (i + 1); e < input.Count; e++)
                            {
                                if (FindType(input[e].TrimStart()[2..].Split(' ').First(), out _))
                                {
                                    IList list = Activator.CreateInstance(typeof(List<>).MakeGenericType(t)) as IList;
                                    foreach(var item in input.GetRange(i, selector))
                                    {
                                        listparse(item, out Type value);
                                        list.Add(value);
                                    }
                                    continue;
                                }
                                selector++;
                            }
                        }
                        break;
                    case "dictionary":
                        break;
                    case "model":
                        break;
                }
                continue;
            }
            FindParser(type, out var parse);

            parse(input[i].Split(':').Last(), out holdingValue);

            result.Add(adder[1], holdingValue);
        }
        return result;
    }
}

