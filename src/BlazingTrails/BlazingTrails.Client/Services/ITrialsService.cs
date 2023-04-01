namespace BlazingTrails.Client.Domain;

using Features.Home.ViewModels;

public interface ITrailsService
{
    Task<IEnumerable<Trail>> GetAllTrailsAsync();
}