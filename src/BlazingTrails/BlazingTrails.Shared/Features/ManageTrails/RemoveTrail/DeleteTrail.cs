using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails.RemoveTrail;

public record DeleteTrailRequest(int TrailId) : IRequest<bool>
{
    public const string RouteTemplate = "/api/trails/{trailId}";
    
}