using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer4.AccessTokenValidation;

namespace IdentityServer.JSApi
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // services.AddAuthentication("Bearer")
            // .AddJwtBearer("Bearer",options=>{
            //     options.Authority="http://localhost:5001";
            //     options.RequireHttpsMetadata=false;

            //     options.Audience="api1";

            // });

            services.AddAuthentication("Bearer")
             .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "http://localhost:5001";
                options.RequireHttpsMetadata = false;

                options.ApiName = "api1";
            });

            services.AddCors(options =>{
                options.AddPolicy("default",policy =>{
                    policy.WithOrigins("http://localhost:5003")
                    .AllowAnyHeader().AllowAnyMethod();

                });

            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseCors("default");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
