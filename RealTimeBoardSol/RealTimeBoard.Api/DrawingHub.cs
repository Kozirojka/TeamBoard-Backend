using Microsoft.AspNetCore.SignalR;
using RealTimeBoard.Domain.EntityNoSQl;
using RealTimeBoard.Infrastructure.NoSQLDatabase;

namespace RealTimeBoard.Api;

public class DrawingHub(MongoDbContext mongoDbContext, ILogger<DrawingHub> logger) : Hub
{

    
    public async Task JoinRoom(string roomName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    }

    public async Task SendVectorObjectToGroup(VectorObject vectorObject)
    {
        
        logger.Log(LogLevel.Information, "Here is come object");
        
        await mongoDbContext.FitureObjects.InsertOneAsync(vectorObject);
        
        await Clients.All.SendAsync("ReceiveVectorObject", vectorObject);
    }
    
    public async Task LeaveRoom(string roomName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
    }
    
    
}