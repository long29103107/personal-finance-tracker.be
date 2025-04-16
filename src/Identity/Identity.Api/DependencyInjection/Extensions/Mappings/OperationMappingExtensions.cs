using Identity.Api.Entities;
using static Shared.Dtos.Identity.SeedDtos;

namespace Identity.Api.DependencyInjection.Extensions.Mappings;

public static class OperationMappingExtensions
{
    public static Operation ToOperation(this OperationRequest OperationRequest)
    {
        return new Operation()
        {
            Code = OperationRequest.Code,
            Name = OperationRequest.Name
        };
    }

    public static List<Operation> ToOperationList(this List<OperationRequest> operationRequests)
    {
        return operationRequests.Select(x => x.ToOperation()).ToList();
    }
}
