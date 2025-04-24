using LegoCollection.DbConnection;
using LegoCollection.Entities;
using LegoCollection.Mapping;
using System.Runtime.CompilerServices;

namespace LegoCollection.Endpoints
{
    public static class ColorsEndpoints
    {
        public static RouteGroupBuilder MapColorsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("colors");

            //GET /colors
            group.MapGet("/", async () =>
            {
                var dbConn = new LegoDbConnection(app.Configuration);
                List<ColorList> colorList = await dbConn.GetColors();

                return colorList.ToColorsDtoList();
            });

            return group; ;
        }
    }
}
