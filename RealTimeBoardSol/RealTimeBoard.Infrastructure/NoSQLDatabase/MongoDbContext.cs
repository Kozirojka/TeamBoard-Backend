using MongoDB.Driver;
using RealTimeBoard.Domain.EntityNoSQl;

namespace RealTimeBoard.Infrustructure.Database;

public class MongoDbContext(IMongoClient mongoClient)
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase("whiteboard");
    
    public IMongoCollection<VectorObject> Orders => _database.GetCollection<VectorObject>("vector-object");
}