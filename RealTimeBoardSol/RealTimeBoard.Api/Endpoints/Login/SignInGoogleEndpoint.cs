using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealTimeBoard.Domain.EntitySQL;

namespace RealTimeBoard.Api.Endpoints.Login;

public class SignInGoogleEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/account/login/google", Handler);
    }

    private async Task<IResult> Handler([FromQuery] string returnUrl, LinkGenerator linkGenerator,
        SignInManager<ApplicationUser> signInManager, HttpContext context)
    {
        var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", 
            linkGenerator.GetPathByName(context, "LoginGoogleCallback") + $"?returnUrl={returnUrl}");

        return Results.Challenge(properties, ["Google"]);
    }
}