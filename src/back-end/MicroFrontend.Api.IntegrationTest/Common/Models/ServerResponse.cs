namespace MicroFrontend.Api.IntegrationTest.Common.Models;

public class ServerResponse<TEntity>
{
    public bool IsSuccessful { get; }
    public ErrorResponse? Error { get; }
    public TEntity? Content { get; }

    /// <summary>
    /// For successful responses
    /// </summary>
    /// <param name="content"></param>
    public ServerResponse(TEntity content)
    {
        IsSuccessful = true;
        Content = content;
        Error = null;
    }

    /// <summary>
    /// For error responses
    /// </summary>
    /// <param name="errorResponse"></param>
    public ServerResponse(ErrorResponse errorResponse)
    {
        IsSuccessful = false;
        Error = errorResponse;
        Content = default(TEntity);
    }
}