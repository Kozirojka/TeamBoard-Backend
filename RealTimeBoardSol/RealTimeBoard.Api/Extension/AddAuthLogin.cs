using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace RealTimeBoard.Api.Extension;

public static class AddAuthLogin
{
    public static void RegisterAddAuthLogin(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = configuration["Authentication:Google:ClientId"] ?? throw new InvalidOperationException();
                googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"] ?? throw new InvalidOperationException();
            })
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.RequireHttpsMetadata = false;
                jwtOptions.SaveToken = true;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"] ?? throw new InvalidOperationException())),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"], 
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();
    }
}