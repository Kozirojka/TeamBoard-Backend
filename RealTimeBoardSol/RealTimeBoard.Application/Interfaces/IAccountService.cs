using System.Security.Claims;
using RealTimeBoard.Domain.Requests;
using RealTimeBoard.Domain.Requests.Auth;

namespace RealTimeBoard.Application.interfaces;

public interface IAccountService
{
    Task RegisterAsync(RegisterRequest registerRequest);
    Task LoginAsync(LoginRequest loginRequest);
    Task RefreshTokenAsync(string? refreshToken);
    Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal);
}