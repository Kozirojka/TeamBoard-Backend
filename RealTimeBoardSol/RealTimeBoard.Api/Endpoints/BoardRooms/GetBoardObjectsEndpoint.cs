using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReadTimeBoard.Application.Board.Query;

namespace RealTimeBoard.Api.Endpoints.BoardRooms;

public class GetBoardObjectsEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/board/{id}", Handler);
    }

    private async Task<IResult> Handler([FromRoute] string id, IMediator mediator)
    {
        var query = new GetBoardQuery();
        
        var result = await mediator.Send(query);
        
        return Results.Ok(result);
    }
}