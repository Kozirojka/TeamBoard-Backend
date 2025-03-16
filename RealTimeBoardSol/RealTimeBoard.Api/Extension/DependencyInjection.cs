using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using RealTimeBoard.Api.Database;

namespace RealTimeBoard.Api.Extension;

public static class DependencyInjection
{
    public static IServiceCollection AddFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        
        services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }
    
    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoConnectionString = configuration.GetConnectionString("MongoDb");
        var mongoClientSettings = MongoClientSettings.FromConnectionString(mongoConnectionString);

        services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));
        services.AddSingleton<MongoDbContext>();

        ConventionRegistry.Register("EnumStringConvention", new ConventionPack
        {
            new EnumRepresentationConvention(BsonType.String)
        }, _ => true);

        ConventionRegistry.Register("camelCase", new ConventionPack {
            new CamelCaseElementNameConvention()
        }, _ => true);

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        return services;
    }


}