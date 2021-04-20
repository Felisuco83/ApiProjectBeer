using ApiProjectBeer.Data;
using ApiProjectBeer.Token;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProjectBeer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProjectBeer
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
            string cadena = this.Configuration.GetConnectionString("cadenaazure");
            services.AddTransient<RepositoryBeer>();
            services.AddDbContext<BeerContext>(x => x.UseSqlServer(cadena));

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(name: "v1", new OpenApiInfo
                {
                    Title = "Api Project Beer",
                    Version = "v1",
                    Description = "Api Project Beer"
                });
            });

            HelperToken helper = new HelperToken(this.Configuration);
            //AÑADIMOS AUTENTIFICACION A NUESTRO SERVICIO
            services.AddAuthentication(helper.GetAuthOptions())
                .AddJwtBearer(helper.GetJwtOptions());

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Api v1");
                x.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
