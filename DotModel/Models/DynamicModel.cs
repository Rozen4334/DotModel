using DotModel.Core;

namespace DotModel.Models;

public abstract class DynamicModel : IDotModel
{
    private readonly string _path;

    private IDotModel Model;

    /// <summary>
    ///     Creates a new disposable instance of your model, passing a valid filepath into it.
    /// </summary>
    /// <param name="path">The path that this instance should be saving to.</param>
    public DynamicModel(FileExtension ex, params string[] path)
    {
        string extension;
        switch (ex)
        {
            case FileExtension.DotModel:
                extension = ".model";
                break;
            case FileExtension.DotTxt:
                extension = ".txt";
                break;
            case FileExtension.DotConfig:
                extension = ".config";
                break;
            default: 
                throw new NotImplementedException();
        }
        if (path[path.Length].Contains(extension))
            path[path.Length] += extension;
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

    async void IDisposable.Dispose()
    {
        await DotSerializer.SerializeAsync(Model, _path);
        GC.SuppressFinalize(this);
    }
}
