using System;
using AutoMapper;
using LibraryManagementAPI.Core.Repositories;
using LibraryManagementAPI.Core.Repositories.Interfaces;
using LibraryManagementAPI.Core.Services;
using LibraryManagementAPI.Core.Services.Interfaces;
using LibraryManagementAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace LibraryManagementAPI.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LibraryContext>(options =>
                options.UseSqlServer
                (
                    Configuration.GetConnectionString("DefaultConnection")
                ).EnableSensitiveDataLogging()
            );

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Library Management API"
                });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IAuthorRepo, AuthorRepo>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IGenreRepo, GenreRepo>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IBookRepo, BookRepo>();
            services.AddScoped<IBookService, BookService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LibraryContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            DataSeed.SeedData(context);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
