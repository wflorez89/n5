using WilmerFlorez.Database;
using WilmerFlorez.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using WilmerFlorez.Commands.EventHandlers.CreatePermisssion;
using MediatR;
using AutoMapper;
using WilmerFlorez.Utilities.Implementation;
using WilmerFlorez.Utilities.Implementation.Middleware;
using WilmerFlorez.Queries.Implementation;
using WilmerFlorez.Common.Kafka;
using WilmerFlorez.Utilities.Implementation.ElasticSearch;

namespace WilmerFlorez.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
            builder.AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTION");
            
            services.AddDbContext<ContextDb>(
                options => options.UseSqlServer(connectionString),
                ServiceLifetime.Scoped);

            services.UseRepository(typeof(ContextDb));

            MapperConfiguration mappingConfig = new MapperConfiguration(config =>
            {
                config.AddMaps("WilmerFlorez.Domain.Configuration");
            });

            services.Configure<KafkaSettings>(Configuration.GetSection("KafkaSettings"));
            services.UseQueryApplication();

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddServices();

            services.AddMediatR(typeof(PermissionCreateCommandHandler).Assembly);

            //Swagger
            AddSwagger(services);


            //ElasticSearch
            services.AddElasticsearch(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ContextDb dataContext)
        {
            app.UsePathBase("/api");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "WilmerFlorez");
            });

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //dataContext.Database.Migrate();
        }



        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"WilmerFlorez {groupName}",
                    Version = groupName,
                    Description = "Wilmer Florez API",
                    Contact = new OpenApiContact
                    {
                        Name = "WilmerFlorez",
                        Email = string.Empty,
                        Url = new Uri("https://wilmerflorez.com/"),
                    }
                });
            });
        }
    }
}
