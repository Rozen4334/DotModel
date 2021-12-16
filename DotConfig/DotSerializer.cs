using System.Reflection;

namespace DotConfig;

internal static class DotSerializer<T> where T : IDotModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="path"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static T Serialize(T model, string path, SerializerSettings? settings = null)
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
    /// <exception cref="NotImplementedException"></exception>
    public static async Task<T> SerializeAsync(T model, string path, SerializerSettings? settings = null)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="path"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static T Deserialize(T model, string path, DeserializerSettings? settings = null)
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
    public static async Task<T> DeserializeAsync(T model, string path, DeserializerSettings? settings = null)
    {
        var properties = typeof(T).GetProperties();

        // Does the type passed contain any properties?
        //
        // TODO: field checks
        if (!properties.Any())
            throw new ArgumentNullException(nameof(properties), "No publically accessible properties found on mentioned type.");

        // Does this file exist?
        if (!File.Exists(path))
            throw new FileNotFoundException("File not found, please check directory:", path);

        // Retrieve the lines from specified path
        var lines = (await File.ReadAllLinesAsync(path)).ToList();

        // Remove all lines that dont matter to us:
        //
        // Comments (starting with *)
        // Whitespace
        lines.RemoveAll(x => string.IsNullOrWhiteSpace(x) || x.StartsWith('*'));

        // Are there any lines left?
        if (!lines.Any())
            throw new ArgumentException("Nothing found in provided file directory.", nameof(path));

        // Iterate through all lines.
        foreach (var line in lines)
        {
            // retrieve entry set
            //
            // TODO: account for lists & dictionaries.
            var array = line.Split(':');

            if (array.Length > 2)
                array[1] = string.Join(':', array[1..]);

            if (array.Length < 2)
            {
                // This is either a dict, list or class. Here we can account for all extra listeners.
            }
            var propertySet = properties.Where(x => x.Name == array[0] && x.CanWrite).ToArray();

            // Technically impossible but are there multiple public properties with the same name?
            if (propertySet.Length > 1)
                throw new ArgumentOutOfRangeException(nameof(propertySet), 
                    "Multiple properties found to assign values to, consider renaming case-sensitive fields to avoid this ambiguity.");

            if (!SerializerExtensions.FindParser(propertySet[0].PropertyType, out var result))
            {
                result(array[1], out var value);
                model.GetType().GetProperty(propertySet[0].Name)?.SetValue(null, value);
            }    
        }
        return model;
    }
}

