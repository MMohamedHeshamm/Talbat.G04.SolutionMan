
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Talabat.Core.Repoisitories.Contract;
using Talabat.Repoistory;
using Talabat.Repoistory.Data;
using Talbat.APIs.Helpers;

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
            //builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly); // for automapper configuration
            builder.Services.AddAutoMapper(typeof(MappingProfiles)); // for automapper configuration
           

            #endregion



            //-------------------------------------------



            var app = builder.Build();



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
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            #endregion





            app.Run();
        }
    }
}
