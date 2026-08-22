
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Talabat.Core.Repoisitories.Contract;
using Talabat.Repoistory;
using Talabat.Repoistory.Data;
using Talbat.APIs.Errors;
using Talbat.APIs.Helpers;
using Talbat.APIs.Middlewares;

namespace Talbat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            #region Configure Services
            // Add services to the container.
            builder.Services.AddControllers();

            //for swagger documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //for database connection
            builder.Services.AddDbContext<StoreContext>
                (options => options.UseSqlServer
                (builder.Configuration.GetConnectionString("Defo")));


            //for dependency injection of the generic repository
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            // for automapper configuration
            builder.Services.AddAutoMapper(typeof(MappingProfiles)); 


            // for validaion error Responce Handlling
            builder.Services.Configure<ApiBehaviorOptions>(options =>
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


            #endregion



            //-------------------------------------------



            var app = builder.Build();


            //for applying the migration and seeding the database with initial data
            #region Update Database

            using var scope = app.Services.CreateScope();

            var Services = scope.ServiceProvider;

            var _dbContext = Services.GetRequiredService<StoreContext>();

            var loggerFactory = Services.GetRequiredService<ILoggerFactory>(); // for logging the error if any error has been occured during applying the migration

            try
            {
                await _dbContext.Database.MigrateAsync();  // for applying the migration to the database
                await StoreContextSeed.SeedAsync(_dbContext); // for seeding the database with initial data

            }
            //for logging the error if any error has been occured during applying the migration
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, "An error has been occured during apply the migration ");

            }






            #endregion


            #region Configure kestrel middlewears
            // Configure HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                //for handling server errors and returning the error response to the client
                app.UseMiddleware<ExceptionMiddleWare>();

                //for swagger documentation
                app.UseSwagger();
                app.UseSwaggerUI();


            }

            app.UseStaticFiles(); // for wwwroot folder to be accessible from the browser

            app.UseHttpsRedirection(); // for redirecting the http request to https request
            app.UseAuthorization();   // for authorization of the request
            app.MapControllers();    // for mapping the controllers to the request

            #endregion





            app.Run(); // for running the application
        }
    }
}
