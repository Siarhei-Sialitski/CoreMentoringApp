using System.Reflection;
using AutoMapper;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Breadcrumbs;
using CoreMentoringApp.WebSite.Cache;
using CoreMentoringApp.WebSite.Email;
using CoreMentoringApp.WebSite.Filters.CustomActionLogger;
using CoreMentoringApp.WebSite.Middlewares;
using CoreMentoringApp.WebSite.Models;
using CoreMentoringApp.WebSite.Options;
using CoreMentoringApp.WebSite.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CoreMentoringApp.WebSite
{
    public class Startup
    {

        private readonly IConfiguration _configuration;
        private readonly string _coreAppRestApiSpecificOrigins = "_coreAppRestApiSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            #region Built-in
            services.AddControllersWithViews();
            services.AddDbContext<NorthwindDbContext>(options =>
            {
                var connectionString = _configuration.GetConnectionString("NorthwindDataContext");
                options.UseSqlServer(connectionString, sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(typeof(NorthwindDbContext).GetTypeInfo().Assembly.GetName().Name);
                    });
            });
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddCors(options =>
            {
                options.AddPolicy(name: _coreAppRestApiSpecificOrigins,
                    builder => { builder.WithOrigins("https://localhost:44351"); });
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo() {Title = "Core Mentoring App Api", Version = "v1"});
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<NorthwindDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.AppId = _configuration["Authentication.Facebook.AppId"];
                    options.AppSecret = _configuration["Authentication.Facebook.AppSecret"];

                });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Home/Login"; 
            });
            services.AddTransient<IEmailSender, MailKitEmailSender>();

            #endregion

            #region Options

            services.AddOptions<ProductViewOptions>()
                .Bind(_configuration.GetSection(ProductViewOptions.ProductView))
                .ValidateDataAnnotations();
            services.AddOptions<CacheOptions>()
                .Bind(_configuration.GetSection(CacheOptions.Cache))
                .ValidateDataAnnotations();
            services.AddOptions<ActionsLoggingOptions>()
                .Bind(_configuration.GetSection(ActionsLoggingOptions.ActionsLogging))
                .ValidateDataAnnotations();
            services.Configure<MailKitOptions>(_configuration);

            #endregion

            #region Singleton

            services.AddSingleton<OptionsConfigurableMemoryCache>();

            #endregion

            #region Scoped

            services.AddScoped<IDataRepository, SqlDataRepository>();
            services.AddScoped<ICustomActionLogger, CustomActionLogger>();

            #endregion

            #region Transient

            services.AddTransient<IStreamMemoryCacheWorker, LocalFileStreamMemoryCacheWorker>();
            services.AddTransient<IBreadcrumbsProvider, BreadcrumbsProvider>();

            #endregion
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core Mentoring App Api v1");
            });

            app.UseRouting();
            app.UseCors(_coreAppRestApiSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCacheMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "images",
                    pattern: "images/{id}",
                    defaults: new {controller = "Categories", action = "Image"});
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
