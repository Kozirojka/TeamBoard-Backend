using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealTimeBoard.Application.Board.Query;

namespace RealTimeBoard.Api.Endpoints.BoardRooms;


/// Точка потрібна для того, щоб користувач міг доступитись до борда, щоб забрати певні дані
public class GetBoardObjectsEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/board/{id}", Handler);

    }

    private async Task<IResult> Handler([FromRoute] Guid id, IMediator mediator)
    {
        var query = new GetBoardQuery(id);
        
        var result = await mediator.Send(query);
        
        return Results.Ok(result);
    }
}