using MediatR;
using MongoDB.Driver;
using RealTimeBoard.Domain.EntityNoSQl;
using RealTimeBoard.Domain.Requests.Board;
using RealTimeBoard.Infrastructure.NoSQLDatabase;

namespace ReadTimeBoard.Application.Board.Query;

// нам потрібно повернути звідси, всі обєкти які є відносні до цієї таблички.

public record GetBoardQuery(Guid BoardId) : IRequest<GetBoardResponce>;


public  class GetBoardQueryHandler(MongoDbContext mongoDbContext) : IRequestHandler<GetBoardQuery, GetBoardResponce>
{
    private readonly MongoDbContext mongoDbContext = mongoDbContext;
    
    public async Task<GetBoardResponce> Handle(GetBoardQuery request, CancellationToken cancellationToken)
    {
        
        var filter = Builders<VectorObject>.Filter.Eq(obj => obj.Id, request.BoardId);

        
        var listOfObjects = await mongoDbContext.FitureObjects.Find(filter).ToListAsync(cancellationToken);

        if (listOfObjects == null || listOfObjects.Count == 0)
        {
            return new GetBoardResponce(new List<VectorObject>());
        }
        
        return new GetBoardResponce(listOfObjects);
    }
}