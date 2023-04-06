using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Api.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails;
using Microsoft.AspNetCore.Mvc;

namespace BlazingTrails.Api.Features.ManageTrails;

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
            Route = request.Trail.Route.Select(r => new RouteInstruction
            {
                Stage = r.Stage,
                Description = r.Description,
            }).ToArray()
        };

        await _context.Trails.AddAsync(trail, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return trail.Id;
    }
}