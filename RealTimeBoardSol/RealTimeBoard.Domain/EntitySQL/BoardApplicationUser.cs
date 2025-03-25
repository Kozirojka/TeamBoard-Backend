using RealTimeBoard.Domain.Enums;

namespace RealTimeBoard.Domain.EntitySQL;

public class BoardApplicationUser
{
    
    public Guid Id { get; set; }
    public Board Board { get; set; }
    public Guid BoardId { get; set; }
    
    public ApplicationUser ApplicationUser { get; set; }
    public Guid ApplicationUserId { get; set; }
    
    
    public Permissions Permissions { get; set; }
}