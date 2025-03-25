using MediatR;
using RealTimeBoard.Application.Board.Command;
using RealTimeBoard.Api.Extension;

namespace RealTimeBoard.Api.Endpoints.BoardRooms;


/// <summary>
/// This is endpoint exist for possibility to connect to board
/// </summary>
public class ConnectToBoardRoomEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/board/join/{token}", Handler).AllowAnonymous();
    }

    private async Task<IResult> Handler(IMediator mediator, Guid token)
    {
        //todo: Потім замінити Id на справнжє
        var result = new JoinToBoardCommand("UserId", token.ToString());
        
        var response = await mediator.Send(result);
        
        if (response.IsError)
        {
            return response.Errors.ToProblem();
        }
        
        return Results.Ok(response.Value);
        //потрінбо перевірити чи користувач атворизований у систему 
        //якщо не авторизований повертаємо назад, і просимо його авторизуватись у систему
    }
}