
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RMotownFestival.Api.Common;
using RMotownFestival.Api.DAL;
using RMotownFestival.Api.Options;
using System;
using System.Configuration;

namespace RMotownFestival.Api
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
            services.AddDbContext<MotownDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                );
                }

            ));
            

            services.Configure<AppSettingsOptions>(Configuration);

            services.AddCors();
            services.AddControllers();
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);

            //Blob (photos et gestion)
            services.AddSingleton(p => new StorageSharedKeyCredential(
                Configuration.GetValue<string>("Storage:AccountName"),
                Configuration.GetValue<string>("Storage:AccountKey")));
            services.AddSingleton(p => new BlobServiceClient(Configuration.GetValue<string>("Storage:ConnectionString")));
            services.AddSingleton<BlobUtility>();
            services.Configure<BlobSettingsOptions>(Configuration.GetSection("Storage"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RMotownFestival.Api", Version = "v1" });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // THIS IS NOT A SECURE CORS POLICY, DO NOT USE IN PRODUCTION
            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RMotownFestival.Api");
            });
        }
    }
}
