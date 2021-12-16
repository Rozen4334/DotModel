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

        if (!properties.Any())
            throw new ArgumentNullException(nameof(properties), "No properties found on mentioned type.");

        if (!File.Exists(path))
            throw new FileNotFoundException("File not found, please check directory:", path);

        var lines = await File.ReadAllLinesAsync(path);

        if (!lines.Any())
            throw new ArgumentException("Nothing found in provided file directory.", nameof(path));

        foreach (var line in lines)
        {
            var array = line.Split(':', StringSplitOptions.TrimEntries);
            var propertySet = properties.Where(x => x.Name == array[0]).ToArray();

            if (propertySet.Length > 1)
                throw new ArgumentOutOfRangeException(nameof(propertySet), 
                    "Multiple properties found to assign values to, consider renaming case-sensitive fields to avoid this ambiguity.");

            if (!propertySet[0].PropertyType.IsGenericType)
            {
                // extract types from the propertytype and assign it this way.
            }
            else
            {
                model.GetType().GetProperty(propertySet[0].Name)?.SetValue(null, array[1]);
            }
        }
        return model;
    }
}

