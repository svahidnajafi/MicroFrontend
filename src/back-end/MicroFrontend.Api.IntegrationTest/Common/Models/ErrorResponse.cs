namespace MicroFrontend.Api.IntegrationTest.Common.Models;

public class ErrorResponse
{
    public ErrorResponse()
    {
            
    }

    public ErrorResponse(string message)
    {
        ErrorMessage = message;
    }
        
    public string ErrorMessage { get; set; }
}