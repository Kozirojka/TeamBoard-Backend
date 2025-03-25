using RealTimeBoard.Application.interfaces;
using RealTimeBoard.Domain.EntityNoSQl;

namespace RealTimeBoard.Application.services;

public class AiService : IAiService
{
    public Task<List<VectorObject>> GenerateVectorObjects(List<VectorObject> vectorObjects)
    {
        throw new NotImplementedException();
    }
}