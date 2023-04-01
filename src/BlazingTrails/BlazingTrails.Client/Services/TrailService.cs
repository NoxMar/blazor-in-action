using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using BlazingTrails.Client.Features.Home.ViewModels;
using Microsoft.Extensions.Logging.Abstractions;

namespace BlazingTrails.Client.Domain;

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
            return await _http.GetFromJsonAsync<IEnumerable<Trail>>("trails/trail-data.json")
                   ?? Array.Empty<Trail>();
        }
        catch (HttpRequestException e)
        {
            _logger.LogWarning(e, "Cannot fetch trails data from the server");
            throw;
        }
    }
}