using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Api.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails.AddTrail;
using Microsoft.AspNetCore.Mvc;

namespace BlazingTrails.Api.Features.ManageTrails.AddTrail;

public class AddTrailEndpoint : EndpointBaseAsync
    .WithRequest<AddTrailRequest>
    .WithResult<int>
{
    private readonly BlazingTrailsContext _context;

    public AddTrailEndpoint(BlazingTrailsContext context)
    {
        _context = context;
    }

    [HttpPost(AddTrailRequest.RouteTemplate)]
    public override async Task<int> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var trail = new Trail
        {
            Name = request.Trail.Name,
            Description = request.Trail.Description,
            Location = request.Trail.Location,
            TimesInMinutes = request.Trail.TimeInMinutes,
            LengthKm = request.Trail.LengthKm,
            Waypoints = request.Trail.Waypoints.Select(w => new Waypoint
            {
                Latitude = w.Latitude,
                Longitude = w.Longitude,
            }).ToArray()
        };

        await _context.Trails.AddAsync(trail, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return trail.Id;
    }
}