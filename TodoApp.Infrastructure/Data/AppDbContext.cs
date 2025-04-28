using __TodoApp.Domain.Entities;
using __TodoApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TodoTasks { get; set; }

      
    }
}
