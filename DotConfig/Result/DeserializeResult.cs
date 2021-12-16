namespace DotConfig;

public struct DeserializeResult
    : IResult
{ 
    public bool IsSuccess { get; }

    public string FilePath { get; }

    public Exception? Exception { get; }

    public DeserializeResult(bool isSuccess, string filepath, Exception? ex = null)
    {
        IsSuccess = isSuccess;
        FilePath = filepath;
        Exception = ex;
    }
}
