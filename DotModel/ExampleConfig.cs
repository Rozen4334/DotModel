using DotModel.Attributes;

namespace DotModel;

public class ExampleClass
{
    public static ExampleConfig Config { get; set; } = new ExampleConfig();
}

public class ExampleConfig 
{
    [DotProperty("Username", "My username")]
    public string Name { get; set; } = "Rozen";

    [DotProperty("Discriminator", "My discriminator")]
    public short Discriminator { get; set; } = 0001;

    public ExampleConfig()
    {

    }
}
