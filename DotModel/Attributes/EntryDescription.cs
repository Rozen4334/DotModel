namespace DotModel.Attributes;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class EntryDescriptionAttribute
: Attribute
{
    public string Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="description"></param>
    public EntryDescriptionAttribute(string description)
    {
        Description = description;
    }
}