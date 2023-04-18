using MediatR;

using BlazingTrails.Shared.Features.ManageTrails.RemoveTrail;
namespace BlazingTrails.Client.Features.ManageTrails.RemoveTrail;

public class RemoveTrailHandler : IRequestHandler<DeleteTrailRequest,
    bool>
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RemoveTrailHandler(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> Handle(DeleteTrailRequest request, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("SecureAPIClient");

        var response = await client.DeleteAsync($"/api/trails/{request.TrailId}", cancellationToken);
        return response.IsSuccessStatusCode;
    }
}