using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Blazored.LocalStorage;

namespace BlazingTrails.Client.AppState;

public class AppState
{
    private bool _isInitalized;
    public NewTrailState NewTrailState { get; } = new();
    public FavoriteTrailsState FavoriteTrailsState { get; }

    public AppState(ILocalStorageService localStorageService)
    {
        FavoriteTrailsState = new FavoriteTrailsState(localStorageService);
    }

    public async Task Initialize()
    {
        if (_isInitalized)
        {
            return;
        }

        await FavoriteTrailsState.Initalize();
        _isInitalized = true;
    }
}