using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Newtonsoft.Json;

namespace DotNetCoreApi_CodeHubGreece
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();
            services.AddDbContext<ModelContext>();
            services.AddMvcCore((options) =>
            {
                options.EnableEndpointRouting = false;
                options.Conventions.Add(new Helpers.GenericODataModelConvention());
            })
                .ConfigureApplicationPartManager(apm => apm.FeatureProviders.Add(new Helpers.GenericODataFeatureProvider()))
                .AddJsonFormatters(options => options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddCors()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc((routeBuilder) =>
            {
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
                routeBuilder.Select().Expand().Filter().OrderBy().Count();
            });
        }

        private static IEdmModel GetEdmModel()
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

            return builder.GetEdmModel();
        }
    }
}
