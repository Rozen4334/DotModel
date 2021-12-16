using DotModel.Core;
using System.Text;

namespace DotModel.Serialization;

public class DotDeserializer
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="path"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static T Deserialize<T>(string path, DeserializerSettings? settings = null)
        where T : IDotModel
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="path"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static async Task<T> DeserializeAsync<T>(string path, DeserializerSettings? settings = null)
        where T : IDotModel
    {
        switch(settings)
        {
            case DeserializerSettings.None:
            default:
                break;
        }

        var returnType = typeof(T);
        var properties = returnType.GetProperties();
        var fields = returnType.GetFields();

        if (!properties.Any() && !fields.Any())
            throw new ArgumentNullException(nameof(properties) + "||" + nameof(fields), 
                "No publically accessible properties nor fields found in provided type.");

        if (!File.Exists(path))
            throw new FileNotFoundException("File not found, please check directory:", path);

        var lines = (await File.ReadAllLinesAsync(path)).ToList();

        lines.RemoveAll(x => string.IsNullOrWhiteSpace(x) || x.StartsWith('*'));

        if (!lines.Any())
            throw new ArgumentNullException(nameof(path), 
                "Nothing found in provided file directory.");

        Dictionary<string, Type> values = SerializerExtensions.FindValuesOfType(lines);

        foreach(var value in values)
        {
            var propertySet = properties.Where(x => x.Name == value.Key && x.CanWrite).ToArray();
            var fieldSet = fields.Where(x => x.Name == value.Key && x.IsPublic).ToArray();
            if (propertySet.Length > 1 || fieldSet.Length > 1 || propertySet.Any(x => x.Name == fieldSet.First().Name))
                throw new ArgumentOutOfRangeException(nameof(propertySet) + " || " + nameof(fieldSet),
                    "Multiple properties or values found to assign values to, consider renaming case-sensitive properties and fields to avoid this ambiguity.");

            returnType.GetProperty(propertySet[0].Name)?.SetValue(null, value.Value);
        }
        return (T)(object)returnType;
    }
}
