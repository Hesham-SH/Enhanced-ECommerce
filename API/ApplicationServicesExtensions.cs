using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API
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

            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
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
            return service;
            }
    }
}