namespace DotConfig;

public enum SaveSettings
{
    DirectlyToFile,

    ToFileOnDispose,

    LocalOnly
}

public enum SerializerSettings
{
    IncludeComments,

    AddWhiteSpaceBetweenProperties,

    DontWriteCaseSensitive,

    IncrementListedItems
}

public enum DeserializerSettings
{
    DontReturnCaseSensitive
}

