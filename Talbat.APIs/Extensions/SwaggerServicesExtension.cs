namespace Talbat.APIs.Extensions
{
    public static class SwaggerServicesExtension
    {




        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {



            //for swagger documentation
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static void UseSwaggerMiddlewares(this WebApplication app)
        {
           
                app.UseSwagger();
                app.UseSwaggerUI();
            
        }
    }
}
