using System;
using System.Diagnostics;

namespace Extensions
{
    public class Result<T>
    {
        public bool IsSuccess { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsFailure => !IsSuccess;

        public T Value { get; }

        public string Error { get; }

        private Result(
            bool isSuccess,
            T value,
            string error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value)
            => new Result<T>(true, value, string.Empty);

        public static Result<T> Failure(string error)
            => new Result<T>(false, default, error);
        
        public static Result<T> Failure(Exception exception)
            => Result<T>.Failure(exception.Message);

        public override string ToString() => IsSuccess
            ? $"Success, Value = {Value}"
            : $"Failure, Error = {Error}";
    }
}