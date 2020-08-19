using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Sky5.SceneService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider => new SqlSugar.ConnectionConfig {
                ConnectionString = "PORT=5432;DATABASE=testdb1;HOST=localhost;PASSWORD=Pg123456;USER ID=postgres",
                DbType = SqlSugar.DbType.PostgreSQL,
                InitKeyType = SqlSugar.InitKeyType.Attribute,
            });
            services.AddSingleton<Satellite.Service>();
            services.AddSingleton<Satellite.ViewAllSatellite>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SceneHub>("scene").();
            });
        }
    }
}
