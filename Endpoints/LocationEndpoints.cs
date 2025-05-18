using LegoCollection.DbConnection;
using LegoCollection.Dtos;
using LegoCollection.Entities;
using LegoCollection.Mapping;

namespace LegoCollection.Endpoints
{
    public static class LocationEndpoints
    {
        public static RouteGroupBuilder MapLocationEndpoints(this WebApplication app)
        {
            const string GetLocationsEndpointName = "GetLocations";
            const string GetLocationByLocationName = "GetLocationByLocation";
            var group = app.MapGroup("locations");

            //GET /locations
            group.MapGet("/", async () =>
            {
                var dbConn = new LocationDbConnection(app.Configuration);
                List<LocationEntity> locationEntity = await dbConn.GetLocations();

                return locationEntity.ToLocationDtoList();
            });

            //GET /locations/id
            group.MapGet("/{id}", async (int id) =>
            {
                var dbConn = new LocationDbConnection(app.Configuration);

                LocationEntity locationEntity  = await dbConn.GetLocationById(id);

                return locationEntity is null ? Results.NotFound() : Results.Ok(locationEntity.ToLocationDto());
            })
                .WithName(GetLocationsEndpointName);

            //GET /locations/location/locationid
            group.MapGet("/location/{id}", async (string id) =>
            {
                var dbConn = new LocationDbConnection(app.Configuration);

                List<LocationEntity> locationEntity = await dbConn.GetLocationByLocationId(id);

                return locationEntity is null ? Results.NotFound() : Results.Ok(locationEntity.ToLocationDtoList());
            })
                .WithName(GetLocationByLocationName);

            // POST /locations
            group.MapPost("/", async (LocationDto createLocation) =>
            {
                var dbConn = new LocationDbConnection(app.Configuration);

                LocationEntity newLocation = createLocation.ToLocationEntity();

                newLocation.Id = await dbConn.AddLocation(newLocation);

                return Results.CreatedAtRoute(GetLocationsEndpointName, new { id = newLocation.LocationId }, newLocation.ToLocationDto());
            });

            //PUT /locations/id
            group.MapPut("/{id}", (int id, LocationDto updatedLocation) =>
            {
                var dbConn = new LocationDbConnection(app.Configuration);

                var existingLocation = dbConn.GetLocationById(id);

                if (existingLocation is null)
                {
                    return Results.NotFound();
                }

                dbConn.UpdateLocation(updatedLocation.ToLocationEntity(id));

                return Results.NoContent();
            });

            //DELETE /locations/id
            group.MapDelete("/{id}", (int id) =>
            {
                var dbConn = new LocationDbConnection(app.Configuration);
                dbConn.DeleteLocation(id);

                return Results.NoContent();
            });

            return group; 
        }
    }
}
