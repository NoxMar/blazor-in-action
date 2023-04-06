using FluentValidation;

namespace BlazingTrails.Shared.Features.ManageTrails;

public class TrailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int TimeInMinutes { get; set; }
    public int LengthKm { get; set; }
    public List<RouteInstruction> Route { get; set; } = new();

    public class RouteInstruction
    {
        public int Stage { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

public class TrailValidator : AbstractValidator<TrailDto>
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
        RuleFor(x => x.Route).NotEmpty()
            .WithMessage("Please add a route instruction");
    }
}