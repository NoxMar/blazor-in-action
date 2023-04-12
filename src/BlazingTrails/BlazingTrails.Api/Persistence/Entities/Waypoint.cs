using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistence.Entities;

public class Waypoint
{
    public int Id { get; set; }
    public int TrailId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public Trail Trail { get; set; } = default!;
}

public class WaypointConfig :
    IEntityTypeConfiguration<Waypoint>
{
    public void Configure(EntityTypeBuilder<Waypoint> builder)
    {
        builder.Property(w => w.TrailId).IsRequired();
        builder.Property(w => w.Latitude).IsRequired();
        builder.Property(w => w.Longitude).IsRequired();
    }
}