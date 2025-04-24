using LegoCollection.DbConnection;
using LegoCollection.Dtos;
using LegoCollection.Entities;
using LegoCollection.Mapping;

namespace LegoCollection.Endpoints
{
    public static class LegoEndpoints
    {
        const string GetOwnedEndpointName = "GetOwned";
        const string GetBrickReportEndpointName = "GetBrickReport";
        const string GetOwnedBrickIdEndpointName = "GetOwnedBrickId";

        public static RouteGroupBuilder MapLegoEndpoints(this WebApplication app)
        {

            var group = app.MapGroup("legos").WithParameterValidation();

            //Full Brick Report Endpoints
            // GET /legos
            group.MapGet("/", () =>
            {
                var dbConn = new LegoDbConnection(app.Configuration);
                return dbConn.GetFullBrickReport();

            });

            //GET /legos/brickid
            group.MapGet("/{brickid}", async (string brickid) =>
            {
                var dbConn = new LegoDbConnection(app.Configuration);

                List<BrickLocation> lego = await dbConn.GetSingleBrickReport(brickid);

                return lego is null ? Results.NotFound() : Results.Ok(lego);
            })
                .WithName(GetBrickReportEndpointName);


            // POST legos/full
            group.MapPost("/full", (FullNewRecord newRecord) =>
            {
                var dbConn = new LegoDbConnection(app.Configuration);

                FullRecordAdd newComplete = new()
                {
                    BrickId = newRecord.BrickId,
                    Count = newRecord.Count,
                    Description = newRecord.Description,
                    Category = newRecord.Category,
                    SubCategory = newRecord.SubCategory,
                    AltBrickId = newRecord.AltBrickId,
                    Color = newRecord.Color,
                    Container = newRecord.Container,
                    Unit = newRecord.Unit,
                    UnitRow = newRecord.UnitRow,
                    Drawer = newRecord.Drawer,
                    Overloaded = newRecord.Overloaded,
                    Underfilled = newRecord.Underfilled,
                    Empty = newRecord.Empty
                };

                dbConn.AddCompleteRecord(newComplete);

                return Results.CreatedAtRoute(GetBrickReportEndpointName, new { id = newComplete.BrickId }, newComplete);
            });

            //Owned Brick Endpoints
            // GET /legos/owned
            group.MapGet("/owned", async () =>
            {
                var dbConn = new LegoDbConnection(app.Configuration);
                                
                List<Owned> allOwned = await dbConn.GetAllOwned();

                return allOwned.ToOwnedBrickSummaryDtoList();                                

            });

            //GET /legos/owned/id
            group.MapGet("/owned/{id}", async (int id) =>
            {
                var dbConn = new LegoDbConnection(app.Configuration);

                Owned ownedBrick = await dbConn.GetOwned(id);

                return ownedBrick is null ? Results.NotFound() : Results.Ok(ownedBrick.ToOwnedBrickDetailsDto());
            })
                .WithName(GetOwnedEndpointName);

            //GET /legos/owned/brick/id
            group.MapGet("/owned/brick/{brickid}", async (string brickid) =>
            {
                var dbConn = new LegoDbConnection(app.Configuration);

                List<Owned> ownedBrick = await dbConn.GetOwnedBrickId(brickid);

                return ownedBrick is null ? Results.NotFound() : Results.Ok(ownedBrick.ToOwnedBrickSummaryDtoList());
            })
                .WithName(GetOwnedBrickIdEndpointName);

            // POST legos/owned
            group.MapPost("/owned", async (CreateOwnedDto newLego) =>
            {
                var dbConn = new LegoDbConnection(app.Configuration);

                Owned newOwned = newLego.ToOwnedEntity();
                //newOwned.Color = dbConn.GetColor(newLego.ColorId);

                newOwned.Id = await dbConn.AddOwnedBrick(newOwned);                                            

                return Results.CreatedAtRoute(GetOwnedEndpointName, new { id = newOwned.BrickId },newOwned.ToOwnedBrickDetailsDto());
            });

            //PUT /legos/owned/id
            group.MapPut("/owned/{id}", (int id, UpdateOwnedDto updatedOwned) =>
            {
                var dbConn = new LegoDbConnection(app.Configuration);

                var existingBrick = dbConn.GetOwned(id);

                if(existingBrick is null)
                {
                    return Results.NotFound();
                }

                dbConn.UpdateOwnedBrick(updatedOwned.ToOwnedEntity(id));

                return Results.NoContent();
            });

            //DELETE /legos/owned/1
            group.MapDelete("/owned/{id}", (int id) =>
            {
                var dbConn = new LegoDbConnection(app.Configuration);
                dbConn.DeleteOwnedBrick(id);

                return Results.NoContent();
            });

            return group;
        }

    }
}
