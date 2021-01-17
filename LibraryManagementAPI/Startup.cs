using System;
using AutoMapper;
using LibraryManagementAPI.Core.Configs;
using LibraryManagementAPI.Core.Exceptions;
using LibraryManagementAPI.Core.Repositories;
using LibraryManagementAPI.Core.Repositories.Interfaces;
using LibraryManagementAPI.Core.Services;
using LibraryManagementAPI.Core.Services.Interfaces;
using LibraryManagementAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;

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

            services.AddControllers(options => options.Filters.Add(new ApiExceptionFilter()))
                    .ConfigureApiBehaviorOptions(options =>
                    {
                        options.SuppressModelStateInvalidFilter = true;
                    })
                    .AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.Configure<ApiOptions>(Configuration.GetSection("Jwt"));

            services.AddScoped<AuthConfigsManager>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            AuthConfigsManager authConfigsManager = serviceProvider.GetService<AuthConfigsManager>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Configuration.GetSection("Jwt").GetValue<string>("issuer"),

                            ValidateAudience = true,
                            ValidAudience = Configuration.GetSection("Jwt").GetValue<string>("audience"),
                            ValidateLifetime = true,

                            IssuerSigningKey = authConfigsManager.GetSymmetricSecurityKey(
                                Configuration.GetSection("Jwt").GetValue<string>("key")),
                            ValidateIssuerSigningKey = true,
                        };
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Library Management API"
                });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthorRepo, AuthorRepo>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IGenreRepo, GenreRepo>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IBookRepo, BookRepo>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserService, UserService>();

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

            

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            DataSeed.SeedData(context);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
