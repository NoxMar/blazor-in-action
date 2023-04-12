using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Contracts;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.Home.Shared;

public class GetTrailsEndpoint : EndpointBaseAsync
    .WithRequest<int>
    .WithResult<GetTrailsRequest.Response>
{
    private readonly BlazingTrailsContext _context;

    public GetTrailsEndpoint(BlazingTrailsContext context)
    {
        _context = context;
    }

    [HttpGet(GetTrailsRequest.RouteTemplate)]
    public override async Task<GetTrailsRequest.Response> HandleAsync(int request, CancellationToken cancellationToken = new CancellationToken())
    {
        var trails = await _context.Trails
            .AsNoTracking()
            .Include(t => t.Waypoints)
            .ToListAsync(cancellationToken);

        var response = new GetTrailsRequest.Response(
            trails.Select(t => new GetTrailsRequest.Trail(
                t.Id,
                t.Name,
                t.Image,
                t.Location,
                t.TimesInMinutes,
                t.LengthKm,
                t.Description,
                t.Waypoints
                    .Select(w => new GetTrailsRequest.WaypointRead(w.Latitude, w.Longitude))
                    .ToList()
            )));
        return response;
    }
}