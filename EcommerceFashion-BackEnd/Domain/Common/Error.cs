using System;

namespace Domain.Common
{
    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3,
    }

    public record Error
    {
        public Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }

        public string Code { get; set; }
        public string Description { get; set; }
        public ErrorType ErrorType { get; set; }

        public static implicit operator Result(Error error) => Result.Failure(error.Description, error);

        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

        public static Error NotFound(string code, string description)
            => new(code, description, ErrorType.NotFound);

        public static Error Validation(string code, string description)
            => new(code, description, ErrorType.Validation);

        public static Error Conflict(string code, string description)
            => new(code, description, ErrorType.Conflict);

        public static Error Failure(string code, string description)
            => new(code, description, ErrorType.Failure);
    }

    public record Error<T> : Error
    {
        public Error(string code, string description, ErrorType errorType)
            : base(code, description, errorType)
        {
        }

        public static implicit operator Result<T>(Error<T> error) => Result<T>.Failure(error.Description, error);

        public static new Error<T> NotFound(string code, string description)
            => new(code, description, ErrorType.NotFound);

        public static new Error<T> Validation(string code, string description)
            => new(code, description, ErrorType.Validation);

        public static new Error<T> Conflict(string code, string description)
            => new(code, description, ErrorType.Conflict);

        public static new Error<T> Failure(string code, string description)
            => new(code, description, ErrorType.Failure);

        public static Error<T> CodeException(string? description = null)
            => new("Internal.Exception", description ?? "InternalServer.Error", ErrorType.Failure);
    }
}
