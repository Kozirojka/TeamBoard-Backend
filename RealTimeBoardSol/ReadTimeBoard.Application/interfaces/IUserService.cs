using RealTimeBoard.Domain.EntitySQL;

namespace ReadTimeBoard.Application.interfaces;

public interface IUserRepository
{
    Task<ApplicationUser?> GetUserByRefreshTokenAsync(string refreshToken);
}