using Microsoft.AspNetCore.Identity;

namespace RealTimeBoard.Domain.EntitySQL;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAtUtc { get; set; }
    
    public string? ProfilePictureUrl { get; set; }
    
    public static ApplicationUser Create(string firstName, string lastName,  string email)
    {
        return new ApplicationUser
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
        };
    }

    public bool AddBoardToUser(Board board)
    {
        
      
        
        return true;
    }
    
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}