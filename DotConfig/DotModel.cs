namespace DotModel;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class DotModel : IDotModel
{
    private readonly string _path;

    private IDotModel Model;

    /// <summary>
    ///     Creates a new disposable instance of your model, passing a valid filepath into it.
    /// </summary>
    /// <param name="path">The path that this instance should be saving to.</param>
    public DotModel(params string[] path)
    {
        // TODO: file extension.
        if (path[path.Length].Contains(".txt"))
            path[path.Length] += ".txt";
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
        var result = DotSerializer.Deserialize(instance, _path);
        return result.Equals(instance) ? DotSerializer.Serialize(instance, _path) : result;
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
        await DotSerializer.SerializeAsync(Model, _path);
        GC.SuppressFinalize(this);
    }
}
