using Microsoft.EntityFrameworkCore;
using RealTimeBoard.Domain.EntitySQL;

namespace RealTimeBoard.Infrustructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<BoardToken> BoardTokens { get; set; }
}