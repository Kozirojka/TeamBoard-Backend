using Microsoft.EntityFrameworkCore;
using ReadTimeBoard.Application.interfaces;
using RealTimeBoard.Domain.EntitySQL;

namespace RealTimeBoard.Infrustructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<ApplicationUser?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        var user = await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

        return user;
    }
}