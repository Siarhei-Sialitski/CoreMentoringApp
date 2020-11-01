using AutoMapper;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Models;
using CoreMentoringApp.WebSite.Options;
using CoreMentoringApp.WebSite.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreMentoringApp.WebSite
{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<NorthwindDbContext>(options =>
            {
                var connectionString = _configuration.GetConnectionString("NorthwindDataContext");
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IDataRepository, SqlDataRepository>();

            services.AddOptions<ProductViewOptions>()
                .Bind(_configuration.GetSection(ProductViewOptions.ProductView))
                .ValidateDataAnnotations();

            services.AddAutoMapper(typeof(AutoMapperProfile));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "images",
                    pattern: "images/{id}",
                    defaults: new {controller = "Category", action = "Image"});
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
