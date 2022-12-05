using System.Text.Json.Serialization;

namespace Panel.DTOs;

public class CustomResponseDto<T>
{
    public T Data { get; set; }

    public List<string> error { get; set; }

    [JsonIgnore]
    public int statusCode { get; set; }

    public static CustomResponseDto<T> success(int statusCode, T data)
    {
        return new CustomResponseDto<T>
        {
            Data = data,
            statusCode = statusCode,
            error = null
        };
    }
    
    public CustomResponseDto<T> success(int statusCode)
    {
        return new CustomResponseDto<T>
        {
            statusCode = statusCode
        };
    }
    
    public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
    {
        return new CustomResponseDto<T>
        {
            statusCode = statusCode,
            error = errors
        };
    }
    
    public static CustomResponseDto<T> Fail(int statusCode, string error)
    {
        return new CustomResponseDto<T>
        {
            statusCode = statusCode,
            error = new List<string> { error }
        };
    }
}