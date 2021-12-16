using DotModel.Core;
using System.Reflection;

namespace DotModel.Results;

public struct AttributeResult : IResult
{
    public bool IsSuccess { get; }

    public CustomAttributeData? AttributeData { get; }

    public Exception? Exception { get; }

    public AttributeResult(bool isSuccess, CustomAttributeData? attributeData, Exception? ex = null)
    {
        IsSuccess = isSuccess;
        AttributeData = attributeData;
        Exception = ex;
    }
}

