using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;

namespace RealTimeBoard.Api.Endpoints.Login;

public class SignInGoogleEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/sign-in/google", Handler);
    }

    private async Task Handler(HttpContext context)
    {
        var redirectUri = "https://localhost:5187/google-response"; 
        var properties = new AuthenticationProperties { RedirectUri = redirectUri };
        await context.ChallengeAsync(GoogleDefaults.AuthenticationScheme, properties);
    }
}