using Microsoft.AspNetCore.SignalR;
using RealTimeBoard.Domain.EntityNoSQl;
using RealTimeBoard.Infrastructure.NoSQLDatabase;

namespace RealTimeBoard.Api;

public class DrawingHub(MongoDbContext mongoDbContext, ILogger<DrawingHub> logger) : Hub
{

   public async Task JoinBoard(string boardId)
   {
       var board = await mongoDbContext.FitureObjects.FindAsync<string>(boardId);
       
       if (board == null)
       {
           await Clients.Caller.SendAsync("Error", "Board not found");
           return;
       }
       
       await Groups.AddToGroupAsync(Context.ConnectionId, boardId);
   }
    public async Task SendVectorObjectToGroup(string boardId, VectorObject vectorObject)
    {
        
        logger.Log(LogLevel.Information, "Here is come object");
        vectorObject.BoardId = Guid.Parse(boardId); 
        
        await mongoDbContext.FitureObjects.InsertOneAsync(vectorObject);
        
        await Clients.Group(boardId).SendAsync("ReceiveVectorObject", vectorObject);
    }
    
    public override async Task OnConnectedAsync()
    {
        logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
    public async Task LeaveRoom(string roomName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
    }
    
    
}