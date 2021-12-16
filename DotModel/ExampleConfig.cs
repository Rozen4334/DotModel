namespace DotModel;

public class ExampleClass
{
    public static ExampleConfig Config { get; set; } = new ExampleConfig().Load();
}

public class ExampleConfig 
    : DotModel
{
    [DotProperty("Username", "My username")]
    public string Name { get; set; } = "Rozen";

    [DotProperty("Discriminator", "My discriminator")]
    public short Discriminator { get; set; } = 0001;

    public ExampleConfig()
    : base("files", "config") 
    {

    }

    public ExampleConfig Load()
        => base.Load(this) as ExampleConfig;

    public void Save()
        => base.Save(this, SaveSettings.DirectlyToFile);
}
