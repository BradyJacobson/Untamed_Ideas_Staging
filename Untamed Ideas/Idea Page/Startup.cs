using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RepoLibrary.Abstraction;

namespace Idea_Page
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string AllMyOrigins = "_allMyOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRepository<Data.Models.Users>, Data.Repositories.UsersRepository>();
            services.AddTransient<IRepository<Data.Models.Ideas>, Data.Repositories.IdeasRepository>();
            services.AddTransient<IRepository<Data.Models.Descriptions>, Data.Repositories.DescriptionsRepository>();
            services.AddTransient<IRepository<Data.Models.Images>, Data.Repositories.ImagesRepository>();
            services.AddTransient<IRepository<Data.Models.Supplies>, Data.Repositories.SuppliesRepository>();
            services.AddTransient<IRepository<Data.Models.Picture>, Data.Repositories.PictureRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy(AllMyOrigins, b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(AllMyOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
