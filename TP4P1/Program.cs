using Microsoft.EntityFrameworkCore;
using TP4P1.Models.DataManager;
using TP4P1.Models.EntityFramework;
using TP4P1.Models.Repository;

namespace TP4P1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IDataRepository<Utilisateur>, UtilisateurManager>();

            builder.Services.AddDbContext<FilmRatingDBContext>(options =>
              options.UseNpgsql(builder.Configuration.GetConnectionString("SeriesDbContextRemote")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}