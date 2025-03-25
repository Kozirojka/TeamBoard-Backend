using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeBoard.Domain.EntitySQL;

namespace RealTimeBoard.Infrastructure.Configurations;

public class BoardApplicationUserConfiguration : IEntityTypeConfiguration<BoardApplicationUser>
{
    public void Configure(EntityTypeBuilder<BoardApplicationUser> builder)
    {
        builder.HasKey(bau => new { bau.BoardId, bau.ApplicationUserId });

        builder.HasOne(bau => bau.Board)
            .WithMany()
            .HasForeignKey(bau => bau.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bau => bau.ApplicationUser)
            .WithMany() 
            .HasForeignKey(bau => bau.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(bau => bau.Permissions)
            .IsRequired();
    }
}