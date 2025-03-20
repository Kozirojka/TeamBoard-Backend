using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using ReadTimeBoard.Application.interfaces; 

namespace RealTimeBoard.Api.Endpoints.Login;

public class GoogleResponceEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/account/login/google/callback", Handler).WithName("LoginGoogleCallback"); 
  
    }

    private async Task<IResult> Handler([FromQuery] string returnUrl, HttpContext context, IAccountService accountService)
    {
        var result = await context.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (!result.Succeeded)
        {
            return Results.Unauthorized();
        }
        
        await accountService.LoginWithGoogleAsync(result.Principal);
        
        return Results.Redirect(returnUrl);
    }
        
}