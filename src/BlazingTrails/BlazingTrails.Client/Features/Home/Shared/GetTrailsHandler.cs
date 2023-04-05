using System.Net.Http.Json;
using BlazingTrails.Shared.Contracts;
using MediatR;

namespace BlazingTrails.Client.Features.Home.Shared;

public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response?>
{
    private readonly HttpClient _httpClient;

    public GetTrailsHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetTrailsRequest.Response?> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await _httpClient
                .GetFromJsonAsync<GetTrailsRequest.Response>(
                    GetTrailsRequest.RouteTemplate, cancellationToken);
        }
        catch (HttpRequestException)
        {
            return default;
        }
    }
}