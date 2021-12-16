namespace DotModel.Core;

public interface IResult
{
    bool IsSuccess { get; }

    Exception? Exception { get; }
}

