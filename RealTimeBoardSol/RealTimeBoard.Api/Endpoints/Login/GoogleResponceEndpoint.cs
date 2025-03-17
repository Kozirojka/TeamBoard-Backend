using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies; // Додано для Cookie-аутентифікації
using RealTimeBoard.Api.Services;

namespace RealTimeBoard.Api.Endpoints.Login;

public class GoogleResponceEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/account/login/google/callback", Handler).WithName("LoginGoogleCallback"); 
  
    }

    private async Task<IResult> Handler(HttpContext context, IJwtService jwtService)
    {
        var info = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        if (info?.Principal != null)
        {
            var userId = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            var token = jwtService.GenerateJwtToken(userId, email); 

            return Results.Ok(new { Token = token });
        }

        return Results.Unauthorized();
    }
}