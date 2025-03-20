using MediatR;
using RealTimeBoard.Domain.EntityNoSQl;
using RealTimeBoard.Domain.Requests.Board;
using RealTimeBoard.Infrustructure.NoSQLDatabase;

namespace ReadTimeBoard.Application.Board.Query;

// нам потрібно повернути звідси, всі обєкти які є відносні до цієї таблички.

public record GetBoardQuery : IRequest<GetBoardResponce>;


public  class GetBoardQueryHandler(MongoDbContext mongoDbContext) : IRequestHandler<GetBoardQuery, GetBoardResponce>
{
    
    
    public async Task<GetBoardResponce> Handle(GetBoardQuery request, CancellationToken cancellationToken)
    {

        return new GetBoardResponce(new List<VectorObject>());
    }
}