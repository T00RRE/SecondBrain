using Microsoft.EntityFrameworkCore;
using SecondBrain.Infrastructure.Data;
using SecondBrain.Infrastructure.Data.Seed;
using SecondBrain.Application.Interfaces;
using SecondBrain.Infrastructure.Repositories;
namespace SecondBrain.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Dodaj DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<DataSeeder>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();
            SeedDatabase(app);

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
        private static void SeedDatabase(IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // 1. Migracja
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    // Ta linia próbuje zastosowaæ wszystkie oczekuj¹ce migracje.
                    context.Database.Migrate();

                    // 2. Seeding
                    var seeder = services.GetRequiredService<DataSeeder>();
                    // Proste, synchroniczne wywo³anie metody asynchronicznej
                    seeder.SeedAsync().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    // W tej fazie rozwoju wystarczy, ¿e b³¹d bêdzie widoczny w konsoli
                    Console.WriteLine($"[SEEDING ERROR]: Database update or seeding failed. {ex.Message}");
                }
            }
        }
    }
}