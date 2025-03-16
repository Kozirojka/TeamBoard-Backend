using Microsoft.AspNetCore.Identity;

namespace RealTimeBoard.Domain.EntitySQL;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    //use will have a list of token. Where each token it's key to canvaboard
    public ICollection<BoardToken> ListOfTokens { get; set; } = new List<BoardToken>();
    
}