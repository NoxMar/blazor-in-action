using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Contracts;
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
            .ToListAsync(cancellationToken);

        var response = new GetTrailsRequest.Response(
            trails.Select(t => new GetTrailsRequest.Trail(
                t.Id,
                t.Name,
                t.Image,
                t.Location,
                t.TimesInMinutes,
                t.LengthKm,
                t.Description
            )));
        return response;
    }
}