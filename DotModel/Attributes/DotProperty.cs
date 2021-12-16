namespace DotModel.Attributes;

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
