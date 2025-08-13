
using Microsoft.EntityFrameworkCore;
using WebApiTest.Repository;

namespace WebApiTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ������������ ������� �� ������ Build()
            builder.Services.AddTransient<ImageRepository>();

            // ��������� ����������� � ������ �������
            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();
            // ����������� EF Core � PostgreSQL
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<ToDoRepository>();

            builder.Services.AddControllers();
            var app = builder.Build();

            // ������������� middleware
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
