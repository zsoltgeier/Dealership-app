using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SJIDON_HFT_2022231.Logic;
using SJIDON_HFT_2022231.Models;
using SJIDON_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICarLogic, CarLogic>();
            services.AddTransient<IBrandLogic, BrandLogic>();
            services.AddTransient<IDealershipLogic, DealershipLogic>();

            services.AddTransient<IRepository<Car>, CarRepository>();
            services.AddTransient<IRepository<Brand>, BrandRepository>();
            services.AddTransient<IRepository<Dealership>, DealershipRepository>();

            services.AddSingleton<DbContext, CarDbContext>();

            services.AddControllers();
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
                endpoints.MapControllers();
            });
        }
    }
}
