using MongoDB.Driver.Linq;
using ReadTimeBoard.Application.interfaces;
using RealTimeBoard.Domain.EntitySQL;
using RealTimeBoard.Infrustructure;

namespace ReadTimeBoard.Application.Repository;

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

    public async Task<ApplicationUser> GetUserByEmail(string email)
    {
        var user = await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            return null;
        }
        
        return user;
    }
}