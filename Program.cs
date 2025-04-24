using LegoCollection.Data;
using LegoCollection.DbConnection;
using LegoCollection.Endpoints;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

//var connString = builder.Configuration.GetConnectionString("DefaultConnection");
//var legoDbConn = new LegoDbConnection(builder.Configuration);

//builder.Services.AddDbContext<LegoCollectionContext>(options =>
//{
//    options.UseMySql(connString, ServerVersion.AutoDetect(connString));
//});
builder.Services.AddScoped<LegoDbConnection>();

var app = builder.Build();

app.MapLegoEndpoints();
app.MapColorsEndpoints();

app.Run();
