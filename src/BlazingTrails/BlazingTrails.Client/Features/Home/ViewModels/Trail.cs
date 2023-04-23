using BlazingTrails.ComponentLibrary.Map;

namespace BlazingTrails.Client.Features.Home.ViewModels;

public class Trail
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int TimeInMinutes { get; set; }
    public string TimeFormatted => $"{TimeInMinutes / 60}h {TimeInMinutes % 60}m";
    public int LengthKm { get; set; }
    public string Owner = string.Empty;
    public IEnumerable<RouteInstruction> Route { get; set; }
    public List<LatLong> Waypoints { get; set; } = new();
}