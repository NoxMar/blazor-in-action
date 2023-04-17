using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.ManageTrails.RemoveTrail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.ManageTrails.RemoveTrail;

public class RemoveTrailEndpoint : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult
{
    private readonly BlazingTrailsContext _databaseContext;

    public RemoveTrailEndpoint(BlazingTrailsContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    [HttpDelete(DeleteTrailRequest.RouteTemplate)]
    public override async Task<ActionResult> HandleAsync(int trailId, CancellationToken cancellationToken = default)
    {
        var trail = await _databaseContext.Trails
            .SingleOrDefaultAsync(t => t.Id == trailId, cancellationToken);
        if (trail is null)
        {
            return NotFound("Trail does not exits");
        }

        _databaseContext.Remove(trail);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}