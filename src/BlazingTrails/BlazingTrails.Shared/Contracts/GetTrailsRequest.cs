using MediatR;

namespace BlazingTrails.Shared.Contracts;

public record GetTrailsRequest() : IRequest<GetTrailsRequest.Response>
{
    public const string RouteTemplate = "/api/trails";

    public record Trail(int Id, string Name, string? Image, string Location, int TimeInMinutes, int Length,
        string Description, string Owner, List<WaypointRead> Waypoints);

    public record WaypointRead(decimal Latitude, decimal Longitude);
    public record Response(IEnumerable<Trail> Trails);
}