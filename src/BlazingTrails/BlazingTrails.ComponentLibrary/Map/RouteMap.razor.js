export function initialize(hostElement, routeMapComponent, existingEndpoints) 
{
    hostElement.map = L.map(hostElement)
        .setView([51.700, -0.10], 3);
 
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
        maxZoom: 18,
        opacity: .75
    }).addTo(hostElement.map);
 
    hostElement.waypoints = [];
    hostElement.lines = [];
    
    if (existingEndpoints && existingEndpoints.length > 0)
    {
        existingEndpoints.forEach(cord =>
        {
            let waypoint = L.marker(cord);
            waypoint.addTo(hostElement.map);
            hostElement.waypoints.push(waypoint);
            let line = L.polyline(hostElement.waypoints
                .map(m => m.getLatLng()),
                { color: 'var(--brand)'})
                .addTo(hostElement.map);
            hostElement.lines.push(line);
        })
    }
    
    if (hostElement.waypoints.length > 0)
    {
        var waypoints = new L.featureGroup(hostElement.waypoints);
        hostElement.map.fitBounds(waypoints
            .getBounds().pad(1));
    }
    
    hostElement.map.on('click', function (e) {
        let waypoint = L.marker(e.latlng);
        waypoint.addTo(hostElement.map);
        hostElement.waypoints.push(waypoint);
        let line = L.polyline(hostElement.waypoints
            .map(m => m.getLatLng()), { color: 'var(--brand)' })
            .addTo(hostElement.map);
        hostElement.lines.push(line);
        
        routeMapComponent.invokeMethodAsync('WaypointAdded', e.latlng.lat, e.latlng.lng);
    });
}

export function deleteLastWaypoint(hostElement)
{
    if (hostElement.waypoints.length > 0)
    {
        const waypoints = hostElement.waypoints;
        let lastWaypoint = waypoints[waypoints.length - 1];
        hostElement.map.removeLayer(lastWaypoint);
        hostElement.waypoints.pop();
        
        if (hostElement.lines.length > 0)
        {
            const lines = hostElement.lines;
            let lastLine = hostElement.lines[lines.length - 1];
            lastLine.remove(hostElement.map);
            hostElement.lines.pop();
        }
        
        const latLon = lastWaypoint.getLatLng();
        return { "Lat": latLon.lat, "Lng": latLon.lng };
    }
}