using Microsoft.AspNetCore.SignalR;
using RealTimeBoard.Domain.EntityNoSQl;

namespace RealTimeBoard.Api;

public class DrawingHub : Hub
{
    public async Task JoinRoom(string roomName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    }

    public async Task SendVectorObjectToGroup(VectorObject vectorObject)
    {
        Console.WriteLine("Hehre was request");
        await Clients.All.SendAsync("ReceiveVectorObject", vectorObject);
    }
    
    
    public async Task LeaveRoom(string roomName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
    }
    
    
}