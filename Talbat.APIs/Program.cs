
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Talabat.Repoistory.Data;

namespace Talbat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            #region Configure Services

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreContext>
                (options => options.UseSqlServer
                (builder.Configuration.GetConnectionString("Defo")));

            #endregion



            //-------------------------------------------



            var app = builder.Build();



            #region Update Database

            using var scope = app.Services.CreateScope();

            var Services = scope.ServiceProvider;

            var _dbContext = Services.GetRequiredService<StoreContext>();

            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbContext);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex,"An error has been occured during apply the migration ");
                
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
