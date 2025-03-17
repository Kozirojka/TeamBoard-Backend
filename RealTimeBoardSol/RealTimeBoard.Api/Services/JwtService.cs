namespace RealTimeBoard.Api.Services;

public class JwtService : IJwtService
{
    public string GenerateJwtToken(string userId, string email)
    {
        throw new NotImplementedException();
    }
}

public interface IJwtService
{
    string GenerateJwtToken(string userId, string email);
}