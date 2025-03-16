using Microsoft.AspNetCore.SignalR;
using RealTimeBoard.Domain.EntityNoSQl;

namespace RealTimeBoard.Api;

public class DrawingHub : Hub
{
    public async Task JoinRoom(string roomName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    }

    public async Task SendVectorObjectToGroup(string roomName, VectorObject vectorObject)
    {
        await Clients.Group(roomName).SendAsync("ReceiveVectorObject", vectorObject);
    }
    
    public async Task LeaveRoom(string roomName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
    }
}