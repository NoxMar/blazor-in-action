using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistence.Entities;

public class Trail
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Image { get; set; }
    public string Location { get; set; } = string.Empty;
    public int TimesInMinutes { get; set; }
    public int LengthKm { get; set; }

    public ICollection<Waypoint> Waypoints { get; set; } = default!;
    public ICollection<RouteInstruction> Route { get; set; } = default!;
}

public class TrailConfig : IEntityTypeConfiguration<Trail>
{
    public void Configure(EntityTypeBuilder<Trail> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Location).IsRequired();
        builder.Property(x => x.TimesInMinutes).IsRequired();
        builder.Property(x => x.LengthKm).IsRequired();
    }
}