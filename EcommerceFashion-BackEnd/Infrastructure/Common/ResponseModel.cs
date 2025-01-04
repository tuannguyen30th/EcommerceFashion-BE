using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Model.ResponseModel
{
    public class ResponseModel
    {
        [JsonIgnore] public int Code { get; set; } = StatusCodes.Status200OK;
        public bool Status => Code >= StatusCodes.Status200OK && Code < 300;
        public string Message { get; set; } = "Success";
        public object? Data { get; set; }
    }
}
