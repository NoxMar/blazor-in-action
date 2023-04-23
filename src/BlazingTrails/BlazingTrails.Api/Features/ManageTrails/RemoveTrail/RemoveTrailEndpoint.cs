using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.ManageTrails.RemoveTrail;
using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize]
    [HttpDelete(DeleteTrailRequest.RouteTemplate)]
    public override async Task<ActionResult> HandleAsync(int trailId, CancellationToken cancellationToken = default)
    {
        var trail = await _databaseContext.Trails
            .SingleOrDefaultAsync(t => t.Id == trailId, cancellationToken);
        if (trail is null)
        {
            return NotFound();
        }

        var user = HttpContext.User.Identity!.Name!;
        if (user != trail.Owner)
        {
            return Unauthorized("You are not authorized to delete this trail");
        }
        _databaseContext.Remove(trail);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}