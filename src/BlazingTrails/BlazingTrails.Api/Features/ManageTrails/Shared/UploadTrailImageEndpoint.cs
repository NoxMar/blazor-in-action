using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.ManageTrails.Shared;

public class UploadTrailImageEndpoint : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<string>
{
    private readonly BlazingTrailsContext _context;

    public UploadTrailImageEndpoint(BlazingTrailsContext context)
    {
        _context = context;
    }

    [HttpPost(UploadTrailImageRequest.RouteTemplate)]
    public override async Task<ActionResult<string>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
    {
        var trail = await _context.Trails
            .SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken);
        if (trail is null)
        {
            return BadRequest("Trail does not exist.");
        }

        var file = Request.Form.Files[0];
        if (file.Length == 0)
        {
            return BadRequest("No image found.");
        }

        var filename = $"{Guid.NewGuid()}.jpg";
        var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "Images", filename);

        ResizeOptions resizeOptions = new()
        {
            Mode = ResizeMode.Pad,
            Size = new(640, 426)
        };

        using var image = await Image.LoadAsync(file.OpenReadStream(), cancellationToken: cancellationToken);
        image.Mutate(x => x.Resize(resizeOptions));
        await image.SaveAsJpegAsync(saveLocation, cancellationToken: cancellationToken);

        trail.Image = filename;
        await _context.SaveChangesAsync(cancellationToken);

        return Ok(trail.Image);
    }
}