using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using NotificationApi.Context;
using NotificationApi.Interfaces;
using NotificationApi.Logging;
using NotificationApi.Models;
using NotificationApi.Repository;


namespace NotificationApi
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
            string connectionString = Configuration.GetConnectionString("ConnectionStr"); 

           // services.AddDbContext<DatabaseContext>(c => c.UseSqlServer(connectionString) );

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
            });

            services.AddScoped<ICustomerAlertSettings, CustomerAlertSettingRepository>();
            services.AddScoped<ICustomerClaimAlert, CustomerClaimAlertRepository>();
            services.AddScoped<ICustomerFullUtilShare, CustomerFullUtilShareRepository>();
            services.AddScoped<ISentStatement, SentStatementRepository>();
            services.AddScoped<IStatementConfig, StatementConfigRepository>();
            services.AddScoped<ICustomerAlert, CustomerAlertRepository<CustomerAlertModel> >();
            services.AddScoped<ISchemeUtilAlert, SchemeUtilAlertRepository>();
            services.AddSingleton<ILoggerManager, LoggerService>();
            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );

            services.AddControllers();
            services.AddResponseCaching();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotificationApi", Version = "v1" });
            });

            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("X-Pagination"));
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{Configuration.GetValue<string>("RedisCache:Host")}:{Configuration.GetValue<int>("RedisCache:Port")}";
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotificationApi v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
