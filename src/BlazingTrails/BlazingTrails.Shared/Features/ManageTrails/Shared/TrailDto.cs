using FluentValidation;

namespace BlazingTrails.Shared.Features.ManageTrails.Shared;

public class TrailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int TimeInMinutes { get; set; }
    public int LengthKm { get; set; }
    public List<Waypoint> Waypoints { get; set; } = new();

    public class Waypoint
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}

public class TrailValidator 
    : AbstractValidator<TrailDto>
{
    public TrailValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage("Please enter a name");
        RuleFor(x => x.Description).NotEmpty()
            .WithMessage("Please enter a description");
        RuleFor(x => x.Location).NotEmpty()
            .WithMessage("Please enter a location");
        RuleFor(x => x.LengthKm).GreaterThan(0)
            .WithMessage("Please enter a length");
        RuleFor(x => x.Waypoints).Must(x => x.Count >= 2)
            .WithMessage("Please add least 2 endpoints");
        RuleFor(x => x.TimeInMinutes).GreaterThan(0)
            .WithMessage("Please enter a time");
    }
}