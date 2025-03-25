using RealTimeBoard.Domain.EntityNoSQl;

namespace RealTimeBoard.Application.interfaces;

public interface IAiService
{
    Task<List<VectorObject>> GenerateVectorObjects(List<VectorObject> vectorObjects); 
}