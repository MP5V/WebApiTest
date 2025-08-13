using Microsoft.EntityFrameworkCore;
using WebApiTest.Models;

namespace WebApiTest
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ToDoItems> ToDoItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItems>().ToTable("todo_items");

            modelBuilder.Entity<ToDoItems>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.IsCompleted).HasColumnName("iscompleted");
                entity.Property(e => e.ImageFileName).HasColumnName("imagefilename");
            });
        }
    }
}
