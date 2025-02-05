using Catalog_Common;
using Catalog_DataAccess;
using Microsoft.EntityFrameworkCore;
using static Catalog_Common.SD;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



var dbConnectionMode = builder.Configuration.GetValue<string>("DbConnectionMode");

DbConnectionMode dbConnectionModeEnum = (DbConnectionMode)Enum.Parse(typeof(DbConnectionMode), dbConnectionMode, true);

switch (dbConnectionModeEnum)
{
    case DbConnectionMode.MSSQL:
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogDBMSSQLConnection"),
                    u => u.CommandTimeout(SD.SqlCommandConnectionTimeout))
                );
            break;
        }
    case DbConnectionMode.PostgreSQL:
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDBPostgresSQLConnection"),
                u => u.CommandTimeout(SD.SqlCommandConnectionTimeout))
            );
            break;
        }
    case DbConnectionMode.SqlLight:
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("CatalogDBSqlLightConnection"),
                u => u.CommandTimeout(SD.SqlCommandConnectionTimeout))
            );
            break;
        }
    default:
        {
            break;
        }
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
