using LegoCollection.DbConnection;
using LegoCollection.Dtos;
using LegoCollection.Entities;
using LegoCollection.Mapping;

namespace LegoCollection.Endpoints
{
    public static class BricksEndpoints
    {
        public static RouteGroupBuilder MapBricksEndpoints(this WebApplication app)
        {
            const string GetBricksEndpointName = "GetBricks";
            const string GetBrickByBrickName = "GetBrickByBrick";
            var group = app.MapGroup("bricks");

            //GET /bricks
            group.MapGet("/", async () =>
            {
                var dbConn = new BricksDbConnection(app.Configuration);
                List<BricksEntity> bricksEntity = await dbConn.GetBricks();

                return bricksEntity.ToBricksDtoList();
            });

            //GET /bricks/id
            group.MapGet("/{id}", async (int id) =>
            {
                var dbConn = new BricksDbConnection(app.Configuration);

                BricksEntity bricksEntity = await dbConn.GetBrickById(id);

                return bricksEntity is null ? Results.NotFound() : Results.Ok(bricksEntity.ToBricksDto());
            })
                .WithName(GetBricksEndpointName);

            //GET /bricks/brick/brickid
            group.MapGet("/brick/{id}", async (string id) =>
            {
                var dbConn = new BricksDbConnection(app.Configuration);

                List<BricksEntity> bricksEntity = await dbConn.GetBrickByBrickId(id);

                return bricksEntity is null ? Results.NotFound() : Results.Ok(bricksEntity.ToBricksDtoList());
            })
                .WithName(GetBrickByBrickName);

            // POST /bricks
            group.MapPost("/", async (BricksDto createBrick) =>
            {
                var dbConn = new BricksDbConnection(app.Configuration);

                BricksEntity newBrick = createBrick.ToBricksEntity();

                newBrick.Id = await dbConn.AddBrick(newBrick);

                return Results.CreatedAtRoute(GetBricksEndpointName, new { id = newBrick.BrickId }, newBrick.ToBricksDto());
            });

            //PUT /bricks/id
            group.MapPut("/{id}", (int id, BricksDto updatedBrick) =>
            {
                var dbConn = new BricksDbConnection(app.Configuration);

                var existingBrick = dbConn.GetBrickById(id);

                if (existingBrick is null)
                {
                    return Results.NotFound();
                }

                dbConn.UpdateBrick(updatedBrick.ToBricksEntity(id));

                return Results.NoContent();
            });

            //DELETE /bricks/id
            group.MapDelete("/{id}", (int id) =>
            {
                var dbConn = new BricksDbConnection(app.Configuration);
                dbConn.DeleteBrick(id);

                return Results.NoContent();
            });

            //GET /bricks/categories
            group.MapGet("/categories", async () =>
            {
                var dbConn = new BricksDbConnection(app.Configuration);
                List<CategoriesEntity> categoriesEntity = await dbConn.GetCategories();

                return categoriesEntity.ToCategoriesDtoList();
            });

            return group;
        }
    }
}
