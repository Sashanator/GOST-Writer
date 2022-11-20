namespace API.Extensions;

public class ApiErrorResponse
{
    /// <summary>
    ///     Error messages where:
    ///     Key : Property name
    ///     Value : Error message
    /// </summary>
    public Dictionary<string, string> Messages { set; get; } = new();

    /// <summary>
    ///     Main error reason
    /// </summary>
    public string MainErrorMessage { set; get; }
}