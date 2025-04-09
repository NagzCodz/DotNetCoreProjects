// Data/TaskDbContext.cs
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using TaskManagementApi.Models;

namespace TaskManagementApi.Data
{
    public class TaskDbContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tasks>().ToTable("Tasks");
        }
    }
}