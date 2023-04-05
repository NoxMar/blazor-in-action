using System.Net.Http.Json;
using BlazingTrails.Client.Domain;
using BlazingTrails.Client.Features.Home.ViewModels;
using BlazingTrails.Shared.Contracts;

namespace BlazingTrails.Client.Services;

public class TrailService : ITrailsService
{
    private readonly HttpClient _http;
    private readonly ILogger<TrailService> _logger;

    public TrailService(HttpClient http, ILogger<TrailService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<IEnumerable<Trail>> GetAllTrailsAsync()
    {
        try
        {
            return (await _http.GetFromJsonAsync<GetTrailsRequest.Response>("/api/trails"))!
                .Trails
                .Select(t => new BlazingTrails.Client.Features.Home.ViewModels.Trail
                {
                    Id = t.Id,
                    Name = t.Name,
                    Image = t.Image,
                    Description = t.Description,
                    Location = t.Location,
                    LengthKm = t.Length,
                    Route = Array.Empty<RouteInstruction>(),
                    TimeInMinutes = t.TimeInMinutes
                });
        }
        catch (HttpRequestException e)
        {
            _logger.LogWarning(e, "Cannot fetch trails data from the server");
            throw;
        }
    }
}