namespace DotConfig;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class DotConfig : IDotModel
{
    private readonly string _path;

    private IDotModel Model;

    /// <summary>
    ///     Creates a new disposable instance of your config model, passing a valid filepath into it.
    /// </summary>
    /// <param name="path">The path that this instance should be saving to.</param>
    public DotConfig(params string[] path)
    {
        if (path[path.Length].Contains(".config"))
            path[path.Length] += ".config";
        _path = Path.Combine(path[..(path.Length - 1)]);

        if (!File.Exists(_path))
            File.Create(_path);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    protected IDotModel Load(IDotModel instance, SerializerSettings? sSettings = null, DeserializerSettings? dSettings = null)
    {
        Model = instance;
        var result = DotSerializer<IDotModel>.Deserialize(instance, _path);
        return result.Equals(instance) ? DotSerializer<IDotModel>.Serialize(instance, _path) : result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    protected IDotModel Save(IDotModel instance, SaveSettings settings, SerializerSettings? sSettings = null, DeserializerSettings? dSettings = null)
    {
        return (Model = instance);
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DotPropertyAttribute
    : Attribute
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public DotPropertyAttribute(string? name = null, string? description = null)
        {
            Description = description;
            Name = name;
        }
    }

    async void IDisposable.Dispose()
    {
        await DotSerializer<IDotModel>.SerializeAsync(Model, _path);
        GC.SuppressFinalize(this);
    }
}
