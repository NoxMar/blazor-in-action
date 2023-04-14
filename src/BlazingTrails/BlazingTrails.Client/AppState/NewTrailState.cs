using BlazingTrails.Shared.Features.ManageTrails.Shared;

namespace BlazingTrails.Client.AppState;

public class NewTrailState
{
    public TrailDto Trail { get; set; } = new();

    public void ClearTrail()
        => Trail = new TrailDto();
}