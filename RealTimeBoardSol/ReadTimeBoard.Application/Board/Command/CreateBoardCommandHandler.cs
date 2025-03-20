using ErrorOr;
using MediatR;
using RealTimeBoard.Domain.Requests.Board;
using RealTimeBoard.Infrustructure;

namespace ReadTimeBoard.Application.Board.Command;


public record CreateBoardCommand(CreateBoardRequest Title, Guid AuthorId) : IRequest<ErrorOr<Success>>;
    
    
public class CreateBoardCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreateBoardCommand, ErrorOr<Success>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ErrorOr<Success>> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var newBoard = new RealTimeBoard.Domain.EntitySQL.Board()
        {
            Id = Guid.NewGuid(),
            Name = request.Title.BoardName,
            AuthorId = request.AuthorId,
        };
        
        await _dbContext.Boards.AddAsync(newBoard, cancellationToken);
        
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result <= 0)
        {
            return Error.Conflict($"Cannot create the board: {request.Title}");
        }

        return new Success();

    }
}