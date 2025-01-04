using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class Result
    {
        protected Result(bool isSuccess, string message, Error error)
        {
            IsSuccess = isSuccess;
            Message = message;
            Error = error;
            
        }
        public static Result Success(string message)
            => new Result(true, message, Error.None);

        public static Result Failure(string message, Error error)
            => new Result(false, message, error);

        [JsonPropertyName("IsSuccess")]
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        [JsonPropertyName("Message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("Error")]
        public Error? Error { get; set; }
    }

    public class Result<T> : Result
    {
       
        public T? Data { get; set; }

        protected Result(bool isSuccess, T data, string message, Error error) : base(isSuccess, message, error)
        {
            Data = data;
        }

        public static Result<T> Success(T value, string message)
        {
            return new Result<T>(true, value, message, Error.None);
        }

        public static new Result<T> Failure(string message, Error error)
        {
            return new Result<T>(false,default, message, error);
        }
    }
}
