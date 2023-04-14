using BlazingTrails.Shared.Features.ManageTrails.Shared;

namespace BlazingTrails.Client.AppState;

public class AppState
{
    public NewTrailState NewTrailState { get; } = new();
}