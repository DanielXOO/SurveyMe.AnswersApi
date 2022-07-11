namespace Answers.Api.Models.Request;

/// <summary>
/// Model for getting page
/// </summary>
public sealed class GetPageRequest
{
    /// <summary>
    /// <value>Page size</value>
    /// </summary>
    public int PageSize { get; set; } = 5;
}