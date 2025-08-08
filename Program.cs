
using WebApiTest.Repository;

namespace WebApiTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Регистрируем сервисы ДО вызова Build()
            builder.Services.AddTransient<DataBaseConnection>();
            builder.Services.AddTransient<ImageRepository>();

            // Добавляем контроллеры и другие сервисы
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();

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
