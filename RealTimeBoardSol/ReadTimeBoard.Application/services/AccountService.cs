using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ReadTimeBoard.Application.interfaces;
using RealTimeBoard.Domain.EntitySQL;
using RealTimeBoard.Domain.Exteptions;
using RealTimeBoard.Domain.Requests;

namespace ReadTimeBoard.Application.services;

public class AccountService : IAccountService
{
    private readonly IAuthTokenProcessor _authTokenProcessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;

    public AccountService(IAuthTokenProcessor authTokenProcessor, UserManager<ApplicationUser> userManager,
        IUserRepository userRepository)
    {
        _authTokenProcessor = authTokenProcessor;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task RegisterAsync(RegisterRequest registerRequest)
    {
        var userExists = await _userManager.FindByEmailAsync(registerRequest.Email) != null;

        if (userExists)
        {
            throw new UserAlreadyExistsException(email: registerRequest.Email);
        }

        var user = ApplicationUser.Create(registerRequest.Email, registerRequest.FirstName, registerRequest.LastName);
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, registerRequest.Password);

        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            throw new RegistrationFailedException(result.Errors.Select(x => x.Description));
        }
    }

    public async Task LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
        {
            throw new LoginFailedException(loginRequest.Email);
        }

        var (jwtToken, expirationDateInUtc) = _authTokenProcessor.GenerateJwtToken(user);
        var refreshTokenValue = _authTokenProcessor.GenerateRefreshToken();

        var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(7);

        user.RefreshToken = refreshTokenValue;
        user.RefreshTokenExpiresAtUtc = refreshTokenExpirationDateInUtc;

        await _userManager.UpdateAsync(user);
        
        _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
        _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);
    }

    public async Task RefreshTokenAsync(string? refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new RefreshTokenException("Refresh token is missing.");
        }

        var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);

        if (user == null)
        {
            throw new RefreshTokenException("Unable to retrieve user for refresh token");
        }

        if (user.RefreshTokenExpiresAtUtc < DateTime.UtcNow)
        {
            throw new RefreshTokenException("Refresh token is expired.");
        }
        
        var (jwtToken, expirationDateInUtc) = _authTokenProcessor.GenerateJwtToken(user);
        var refreshTokenValue = _authTokenProcessor.GenerateRefreshToken();

        var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(7);

        user.RefreshToken = refreshTokenValue;
        user.RefreshTokenExpiresAtUtc = refreshTokenExpirationDateInUtc;

        await _userManager.UpdateAsync(user);
        
        _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
        _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);
    }

    public async Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal)
    {
        if (claimsPrincipal == null)
        {
            throw new ExternalLoginProviderException("Google", "ClaimsPrincipal is null");
        }
        
        var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

        if (email == null)
        {
            throw new ExternalLoginProviderException("Google", "Email is null");
        }

        var user = await _userRepository.GetUserByEmail(email);

        if (user == null)
        {
            var newUser = new ApplicationUser()
            {
                UserName = email,
                Email = email,
                FirstName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
                LastName = claimsPrincipal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser);

            if (!result.Succeeded)
            {
                throw new ExternalLoginProviderException("Google",
                    $"Unable to create user: {string.Join(", ",
                        result.Errors.Select(x => x.Description))}");
            }

            

            user = newUser;
        }
        
       var providerKey = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
       
           var existingLogins = await _userManager.GetLoginsAsync(user);
           var alreadyLinked = existingLogins.Any(l => l.LoginProvider == "Google" && l.ProviderKey == providerKey);
       
           if (!alreadyLinked)
           {
               var info = new UserLoginInfo("Google", providerKey, "Google");
               var loginResult = await _userManager.AddLoginAsync(user, info);
       
               if (!loginResult.Succeeded)
               {
                   throw new ExternalLoginProviderException("Google",
                       $"Unable to login user: {string.Join(", ", loginResult.Errors.Select(x => x.Description))}");
               }
           }
        
        var (jwtToken, expirationDateInUtc) = _authTokenProcessor.GenerateJwtToken(user);
        var refreshTokenValue = _authTokenProcessor.GenerateRefreshToken();

        var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(7);

        user.RefreshToken = refreshTokenValue;
        user.RefreshTokenExpiresAtUtc = refreshTokenExpirationDateInUtc;

        await _userManager.UpdateAsync(user);
        
        _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
        _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);
    }
}