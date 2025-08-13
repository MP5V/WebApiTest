
using Microsoft.EntityFrameworkCore;
using WebApiTest.Repository;

namespace WebApiTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Регистрируем сервисы ДО вызова Build()
            builder.Services.AddTransient<ImageRepository>();

            // Добавляем контроллеры и другие сервисы
            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();
            // Подключение EF Core с PostgreSQL
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<ToDoRepository>();

            builder.Services.AddControllers();
            var app = builder.Build();

            // Конфигурируем middleware
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
