namespace RealTimeBoard.Api;

/// interface for mapping minimal api
public interface IEndpoint
{
    void RegisterEndpoints(IEndpointRouteBuilder endpoints);
}