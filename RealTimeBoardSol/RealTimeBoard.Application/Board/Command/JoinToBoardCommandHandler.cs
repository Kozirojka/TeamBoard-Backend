using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealTimeBoard.Domain.EntitySQL;
using RealTimeBoard.Infrastructure;

namespace RealTimeBoard.Application.Board.Command;

public record JoinToBoardCommand(string UserId, string InviteLink) : IRequest<ErrorOr<Success>>;
    
    
public class JoinToBoardCommandHandler : IRequestHandler<JoinToBoardCommand, ErrorOr<Success>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    public JoinToBoardCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Success>> Handle(JoinToBoardCommand request, CancellationToken cancellationToken)
    {
        var boardTask = _dbContext.Boards.SingleOrDefaultAsync(u => u.InviteLink == request.InviteLink, cancellationToken);
        var userTask = _userManager.FindByIdAsync(request.UserId);

        await Task.WhenAll(boardTask, userTask);

        var board = await boardTask;
        var user = await userTask;

        if (board == null || user == null)
        {
            return Error.NotFound("General.NotFound", board == null ?
                "There does not exist such a board" 
                : "There does not exist such a user");
        }
        
        // робимо так, добавляєм таблицю до користувача
        
        user.AddBoardToUser(board);

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        // потрібно створити якось штуку яка буде додавати користувача до дошки
        
        
        return new Success();
    }
}