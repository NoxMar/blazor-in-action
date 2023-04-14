using BlazingTrails.Shared.Features.ManageTrails.Shared;

namespace BlazingTrails.Client.AppState;

public class AppState
{
    public TrailDto Trail { get; set; } = new();

    public void ClearTrail()
        => Trail = new TrailDto();
}