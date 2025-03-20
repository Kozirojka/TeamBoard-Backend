using MediatR;
using ReadTimeBoard.Application.Board.Command;
using RealTimeBoard.Api.Extension;
using RealTimeBoard.Domain.Requests.Board;

namespace RealTimeBoard.Api.Endpoints.BoardRooms;

public class CreateBoardRoomEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/board", Handler);
    }

    private async Task<IResult> Handler(IMediator mediator, CreateBoardRequest createBoardRequest)
    {
       
        var command = new CreateBoardCommand(createBoardRequest, new Guid("mockId"));

        var result = await mediator.Send(command);
        
        if (result.IsError)
        {
            return result.Errors.ToProblem();
        }
        
        return Results.Ok(result.Value);
    }
}
