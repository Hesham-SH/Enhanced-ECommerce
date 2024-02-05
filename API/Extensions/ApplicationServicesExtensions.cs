using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service, IConfiguration config)
        {
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();

            service.AddDbContext<SiteContext>(opt => 
            {
                opt.UseSqlServer(config.GetConnectionString("Connection"));
            });

            service.AddSingleton<IConnectionMultiplexer>(connection => {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });

            service.AddScoped<IBasketRepository, BasketRepository>();
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            service.Configure<ApiBehaviorOptions>(opt => 
            {
                opt.InvalidModelStateResponseFactory = actionContext => 
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidation
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            service.AddCors( opt => 
            {
                    opt.AddPolicy("CorsPolicy", policy => 
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                    });
            });

            return service;
        }
    }

    
}