namespace RealTimeBoard.Domain.EntitySQL;

public class Board
{
    public Guid Id { get; set; }
    public string BoardName { get; set; }
    public Guid AuthorId { get; set; }
    public ApplicationUser Author {get; set;}
    
    
    //this is invite link for users
    public string InviteLink { get; set; }
}