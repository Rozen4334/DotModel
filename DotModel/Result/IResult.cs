namespace DotModel;

public interface IResult
{
    bool IsSuccess { get; }

    Exception? Exception { get; }
}

