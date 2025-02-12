using Catalog_Business.Repository;
using Catalog_Business.Repository.IRepository;
using Catalog_Common;
using Catalog_DataAccess;
using Catalog_DataAccess.DbInitializer;
using Microsoft.EntityFrameworkCore;
using static Catalog_Common.SD;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Catalog_WebAPI
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddMvcOptions(x =>
                x.SuppressAsyncSuffixInActionNames = false);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookInstanceRepository, BookInstanceRepository>();
            services.AddScoped<IBookToAuthorRepository, BookToAuthorRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();


            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var dbConnectionMode = Configuration.GetValue<string>("DbConnectionMode");

            DbConnectionMode dbConnectionModeEnum = (DbConnectionMode)Enum.Parse(typeof(DbConnectionMode), dbConnectionMode, true);
            SD.dbConnectionMode = dbConnectionModeEnum;

            switch (dbConnectionModeEnum)
            {
                case DbConnectionMode.MSSQL:
                    {
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseSqlServer(Configuration.GetConnectionString("CatalogDBMSSQLConnection"),
                            u => u.CommandTimeout(SD.SqlCommandConnectionTimeout));
                            options.UseLazyLoadingProxies();
                        });

                        break;
                    }
                case DbConnectionMode.PostgreSQL:
                    {
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseNpgsql(Configuration.GetConnectionString("CatalogDBPostgresSQLConnection"),
                            u => u.CommandTimeout(SD.SqlCommandConnectionTimeout));
                            options.UseLazyLoadingProxies();
                        });
                        break;
                    }
                case DbConnectionMode.SqlLight:
                    {
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseSqlite(Configuration.GetConnectionString("CatalogDBSqlLightConnection"),
                            u => u.CommandTimeout(SD.SqlCommandConnectionTimeout));
                            options.UseLazyLoadingProxies();
                        });
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            //services.AddOpenApiDocument(options =>
            //{
            //    options.Title = "Catalog (Library API Doc)";
            //    options.Version = "1.0";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseOpenApi();         

            //app.UseSwaggerUI(x =>
            //{
            //    //x.DocExpansion = "list";
            //    x.SwaggerEndpoint("/openapi/v1.json", "Catalog API ver. 1");
            //});

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbInitializer.InitializeDb();
        }
    }
}
