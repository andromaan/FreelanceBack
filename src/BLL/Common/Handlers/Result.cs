namespace BLL.Common.Handlers;

/// <summary>
/// Represents a result that can be either a success (TSuccess) or a failure (TFailure).
/// Used in handlers to return either a processed entity or an error response.
/// </summary>
public class Result<TSuccess, TFailure>
{
    private readonly TSuccess? _success;
    private readonly TFailure? _failure;

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    private Result(TSuccess? success)
    {
        _success = success;
        _failure = default;
        IsSuccess = true;
    }

    private Result(TFailure failure)
    {
        _success = default;
        _failure = failure;
        IsSuccess = false;
    }

    /// <summary>
    /// Creates a successful result with the entity.
    /// </summary>
    public static Result<TSuccess, TFailure> Success(TSuccess? value) => new(value);

    /// <summary>
    /// Creates a failed result with the error response.
    /// </summary>
    public static Result<TSuccess, TFailure> Failure(TFailure error) => new(error);

    /// <summary>
    /// Gets the success value. Throws if result is not successful.
    /// </summary>
    public TSuccess? GetSuccess() => 
        IsSuccess ? _success : throw new InvalidOperationException("Result is not successful");

    /// <summary>
    /// Gets the failure value. Throws if result is successful.
    /// </summary>
    public TFailure GetFailure() => 
        IsFailure ? _failure! : throw new InvalidOperationException("Result is not a failure");

    /// <summary>
    /// Pattern matching for result handling.
    /// </summary>
    public TResult Match<TResult>(
        Func<TSuccess, TResult> onSuccess,
        Func<TFailure, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(_success!) : onFailure(_failure!);
    }
}
