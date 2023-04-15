using BlazingTrails.Client.Features.Home.ViewModels;
using Blazored.LocalStorage;

namespace BlazingTrails.Client.AppState;

public class FavoriteTrailsState
{
    private const string FavoriteTrailsKey = "favoriteTrails";
    private bool _isInitialized = false;
    private List<Trail> _favoriteTrails = new();
    private readonly ILocalStorageService _localStorageService;
    public IReadOnlyList<Trail> FavoriteTrails 
        => _favoriteTrails.AsReadOnly();

    public event Action? OnChange;

    public FavoriteTrailsState(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task Initalize()
    {
        if (_isInitialized)
        {
            return;
        }

        _favoriteTrails = await _localStorageService.GetItemAsync<List<Trail>>(FavoriteTrailsKey)
                          ?? new List<Trail>();
        _isInitialized = true;
        NotifyStateHasChanged();
    }

    public async Task AddFavorite(Trail trail)
    {
        if (_favoriteTrails.Any(t => t.Id == trail.Id))
        {
            return;
        }

        _favoriteTrails.Add(trail);
        await _localStorageService.SetItemAsync(FavoriteTrailsKey, _favoriteTrails);
        NotifyStateHasChanged();
    }

    public async Task RemoveFavorite(Trail trail)
    {
        var trailToRemove = _favoriteTrails.SingleOrDefault(t => t.Id == trail.Id);
        if (trailToRemove is null)
        {
            return;
        }

        _favoriteTrails.Remove(trailToRemove);
        await _localStorageService.SetItemAsync(FavoriteTrailsKey, _favoriteTrails);
        NotifyStateHasChanged();
    }


    public bool IsFavorite(Trail trail)
        => _favoriteTrails.Any(t => t.Id == trail.Id);
    private void NotifyStateHasChanged()
        => OnChange?.Invoke();
}