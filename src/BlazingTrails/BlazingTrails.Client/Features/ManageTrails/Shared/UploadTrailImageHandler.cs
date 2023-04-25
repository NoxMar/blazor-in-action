using BlazingTrails.Shared.Features.ManageTrails;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using MediatR;

namespace BlazingTrails.Client.Features.ManageTrails.Shared;

// ReSharper disable once UnusedType.Global due to begin MediatR handler
public class UploadTrailImageHandler :
    IRequestHandler<UploadTrailImageRequest, UploadTrailImageRequest.Response>
{
    private readonly HttpClient _httpClient;

    public UploadTrailImageHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UploadTrailImageRequest.Response> Handle(UploadTrailImageRequest request, CancellationToken cancellationToken)
    {
        var fileContent = request.File
            .OpenReadStream(request.File.Size, cancellationToken);
        
        using MultipartFormDataContent content = new();
        content.Add(new StreamContent(fileContent),
            "image", request.File.Name);

        var response = await _httpClient
            .PostAsync(UploadTrailImageRequest.RouteTemplate
                    .Replace("{trailId}", request.TrailId.ToString()),
                content, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return new UploadTrailImageRequest.Response(string.Empty);
        }
        
        var fileName = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
        return new UploadTrailImageRequest.Response(fileName);

    }
}