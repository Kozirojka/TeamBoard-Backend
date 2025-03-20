namespace RealTimeBoard.Domain.EntitySQL;

public class Board
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid AuthorId { get; set; }
}