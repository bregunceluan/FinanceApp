
using FinanceApp.Core.Libraries.Consts;
using System.Text.Json.Serialization;

namespace FinanceApp.Core.Responses;
public class Response<T>
{
    private readonly int _statusCode;

    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;

    [JsonIgnore]
    public bool IsSuccess => _statusCode is >= 200 and <= 288;

    [JsonConstructor]
    public Response() => _statusCode = Configuration.DefaultStatusCodeResponse;


    public Response(T? data, string? message = null, int code = Configuration.DefaultStatusCodeResponse)
    {
        _statusCode = code;
        Data = data;
        Message = message;
    }

}
