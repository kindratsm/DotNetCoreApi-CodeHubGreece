using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DotNetCoreApi_CodeHubGreece
{
    public class Startup
    {
        private readonly String JsonPath;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            JsonPath = Path.Combine(System.AppContext.BaseDirectory, "json");
            Directory.CreateDirectory(JsonPath);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Init OData
            services.AddOData();

            // Init DBContext dependency injection
            services.AddDbContext<ModelContext>();

            // Init CORS
            services.AddCors();

            // Init MVC
            services.AddMvcCore((options) =>
            {
                options.EnableEndpointRouting = false;
                options.Conventions.Add(new Helpers.GenericODataModelConvention());
                options.OutputFormatters.Insert(0, new Helpers.CustomODataOutputFormatter());
            })
                .ConfigureApplicationPartManager(apm => apm.FeatureProviders.Add(new Helpers.GenericODataFeatureProvider()))
                .AddJsonFormatters(options => options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddApiExplorer()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger generator
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(JsonPath),
                RequestPath = "/json"
            });
            app.UseHsts();
            app.UseCors((builder) =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseMvc((routeBuilder) =>
            {
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
                routeBuilder.Select().Expand().Filter().OrderBy().MaxTop(null).Count();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI((options) =>
            {
                options.SwaggerEndpoint("/json/swagger.json", "OData");
            });
        }

        private IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            // Register entities which has GeneratedController attribute
            MethodInfo registerMethod = builder.GetType().GetMethod("EntitySet", new Type[] { typeof(String) });

            var currentAssembly = typeof(Startup).Assembly;
            var candidates = currentAssembly.GetExportedTypes().Where(x => x.GetCustomAttributes<Helpers.GeneratedControllerAttribute>().Any());

            foreach (var candidate in candidates)
            {
                MethodInfo genericMethod = registerMethod.MakeGenericMethod(candidate);
                genericMethod.Invoke(builder, new object[] { candidate.Name });
            }

            var model = builder.GetEdmModel();

            var converter = new ODataSwaggerConverter(model);
            var swaggerModel = converter.GetSwaggerModel();
            var swaggerPaths = (JObject)swaggerModel.GetValue("paths");

            List<JProperty> childrenToRemove = new List<JProperty>();

            foreach (var property in swaggerPaths.Properties())
            {
                var path = property.Value<JToken>();
                var obj = (JObject)path.FirstOrDefault();
                foreach (var child in obj.Children<JProperty>())
                {
                    if (child.Name == "patch")
                    {
                        childrenToRemove.Add(child);
                    }
                }
            }

            childrenToRemove.ForEach((child) =>
            {
                child.Remove();
            });

            var swaggerPath = Path.Combine(JsonPath, "swagger.json");
            File.WriteAllText(swaggerPath, converter.GetSwaggerModel().ToString(), Encoding.UTF8);

            return model;
        }
    }
}
