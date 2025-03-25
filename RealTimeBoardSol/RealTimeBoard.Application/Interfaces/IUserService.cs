using RealTimeBoard.Domain.EntitySQL;

namespace RealTimeBoard.Application.interfaces;

public interface IUserRepository
{
    Task<ApplicationUser?> GetUserByRefreshTokenAsync(string refreshToken);
    Task<ApplicationUser> GetUserByEmail(string email);
}