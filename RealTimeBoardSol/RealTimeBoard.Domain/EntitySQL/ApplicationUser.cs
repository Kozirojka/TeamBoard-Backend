using Microsoft.AspNetCore.Identity;

namespace RealTimeBoard.Domain.EntitySQL;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAtUtc { get; set; }

    public ICollection<Board> Boards { get; set; }
    
    public static ApplicationUser Create(string firstName, string lastName,  string email)
    {
        return new ApplicationUser
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
        };
    }
    //use will have a list of token. Where each token it's key to canvaboard

    public bool AddBoardToUser(Board board)
    {
        
        if (board == null)
        {
            throw new ArgumentNullException(nameof(board), "Board cannot be null");
        }
        
        Boards.Add(board);
        
        return true;
    }
    
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}