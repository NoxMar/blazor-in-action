using BlazingTrails.ComponentLibrary.Map;
using BlazingTrails.Shared.Contracts;
using Mapster;

namespace BlazingTrails.Client;

public class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetTrailsRequest.Trail, Features.Home.ViewModels.Trail>()
            .Map(t => t.LengthKm, t => t.Length);
        
        config.NewConfig<GetTrailsRequest.WaypointRead, LatLong>()
            .Map(ll => ll.Lat, way => way.Latitude)
            .Map(ll => ll.Lng, way => way.Longitude);
    }
}