namespace DotConfig;

public struct SerializeResult
    : IResult
{
    public string FilePath { get; }

    public bool IsSuccess { get; }

    public Exception? Exception { get; }

    internal SerializeResult(bool isSuccess, string filePath, Exception ? ex = null)
    {
        IsSuccess = isSuccess;
        FilePath = filePath;
        Exception = ex;
    }
}

