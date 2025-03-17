namespace RealTimeBoard.Domain.EntitySQL;

public class BoardToken
{
    public int Id { get; set; }
    public string Token { get; set; } 
    public DateTime Expires { get; set; } 
    public bool IsRevoked { get; set; } 

    public required string ApplicationUserId { get; set; }
    public required ApplicationUser ApplicationUser { get; set; }
}