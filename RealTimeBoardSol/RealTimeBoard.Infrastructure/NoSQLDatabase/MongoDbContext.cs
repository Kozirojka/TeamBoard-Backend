using MongoDB.Driver;
using RealTimeBoard.Domain.EntityNoSQl;

namespace RealTimeBoard.Infrastructure.NoSQLDatabase;

public class MongoDbContext(IMongoClient mongoClient)
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase("whiteboard");
    
    public IMongoCollection<VectorObject> FitureObjects => _database.GetCollection<VectorObject>("vector-object");
}