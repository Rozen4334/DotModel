using System.Reflection;
using DotModel.Core;

namespace DotModel.Serialization;

internal static class DotSerializer
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="path"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static T Serialize<T>(T model, string path, SerializerSettings? settings = null) 
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
    /// <exception cref="NotImplementedException"></exception>
    public static async Task<T> SerializeAsync<T>(T model, string path, SerializerSettings? settings = null) 
        where T : IDotModel
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}

