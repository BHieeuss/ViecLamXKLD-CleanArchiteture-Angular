namespace ViecLam.Application.Response
{
    public record ServiceResponse(bool IsSuccess, string Message, int StatusCode, List<string>? Errors = null, IReadOnlyList<object>? Data = null)
    {
    }
}
