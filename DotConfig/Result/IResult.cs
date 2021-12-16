namespace DotConfig;

public interface IResult
{
    bool IsSuccess { get; }

    Exception? Exception { get; }
}

