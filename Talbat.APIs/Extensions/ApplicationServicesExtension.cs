using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Repoisitories.Contract;
using Talabat.Repoistory;
using Talbat.APIs.Errors;
using Talbat.APIs.Helpers;

namespace Talbat.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {


        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            //for dependency injection of the generic repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            // for automapper configuration
            services.AddAutoMapper(typeof(MappingProfiles));


            // for validaion error Responce Handlling
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState
                        .Where(p => p.Value.Errors.Count() > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    var ValidationErrorResponse = new ApiValidationErrorResponce
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });

            return services;
        }
    }
}
