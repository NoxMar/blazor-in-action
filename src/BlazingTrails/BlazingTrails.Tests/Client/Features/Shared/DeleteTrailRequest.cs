using System.Threading;
using System.Threading.Tasks;
using BlazingTrails.Shared.Features.ManageTrails.RemoveTrail;
using MediatR;

namespace BlazingTrails.Tests.Client.Features.Shared;

public class DeleteTrailHandler : IRequestHandler<DeleteTrailRequest, bool>
{
    public Task<bool> Handle(DeleteTrailRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}