using Catalog_Business.Repository;
using Catalog_Business.Repository.IRepository;
using Catalog_Common;
using Catalog_DataAccess;
using Catalog_DataAccess.DbInitializer;
using Catalog_WebAPI.Controllers;
using Catalog_WebAPI.Controllers.Extensions;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
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
            services.AddSwaggerGen(c =>
            {
                // include API xml documentation
                var apiAssembly = typeof(AuthorsController).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(apiAssembly));

                apiAssembly = typeof(PublishersController).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(apiAssembly));

                apiAssembly = typeof(StatesController).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(apiAssembly));

                // include models xml documentation
                var modelsAssembly = typeof(Catalog_Models.CatalogModels.Author.AuthorItemCreateUpdateRequest).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));
                modelsAssembly = typeof(Catalog_Models.CatalogModels.Author.AuthorItemResponse).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));

                modelsAssembly = typeof(Catalog_Models.CatalogModels.Publisher.PublisherItemCreateUpdateRequest).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));
                modelsAssembly = typeof(Catalog_Models.CatalogModels.Publisher.PublisherItemResponse).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));

                modelsAssembly = typeof(Catalog_Models.CatalogModels.State.StateItemCreateRequest).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));
                modelsAssembly = typeof(Catalog_Models.CatalogModels.State.StateItemResponse).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));
                modelsAssembly = typeof(Catalog_Models.CatalogModels.State.StateItemUpdateRequest).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));

                c.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Catalog sevice API (Library)", Version = "v1" });

            });

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookInstanceRepository, BookInstanceRepository>();
            services.AddScoped<IBookToAuthorRepository, BookToAuthorRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("CatalogDBPostgresSQLConnection"),
                u => u.CommandTimeout(SD.SqlCommandConnectionTimeout));
                options.UseLazyLoadingProxies();
            });


            services.AddOpenApiDocument(options =>
            {
                options.Title = "Catalog (Library API Doc)";
                options.Version = "1.0";
            });
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

            app.UseOpenApi();

            app.UseSwaggerUI(x =>
            {
                x.DocExpansion(DocExpansion.List);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbInitializer.InitializeDb();
        }

        private static string GetXmlDocumentationFileFor(Assembly assembly)
        {
            var documentationFile = $"{assembly.GetName().Name}.xml";
            var path = Path.Combine(AppContext.BaseDirectory, documentationFile);

            return path;
        }
    }
}
