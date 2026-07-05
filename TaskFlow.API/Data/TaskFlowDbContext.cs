using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;

namespace TaskFlow.API.Data;

public class TaskFlowDbContext : DbContext
{
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Projects)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Project>()
            .HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId);

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Admin", Email = "admin@taskflow.com", PasswordHash = "admin123", Role = "Admin" },
            new User { Id = 2, Name = "Manager", Email = "manager@taskflow.com", PasswordHash = "manager123", Role = "Manager" }
        );

        modelBuilder.Entity<Project>().HasData(
            new Project { Id = 1, Name = "Website Redesign", Description = "Redesign company website", UserId = 2 },
            new Project { Id = 2, Name = "Mobile App", Description = "Build iOS/Android app", UserId = 2 }
        );

        modelBuilder.Entity<TaskItem>().HasData(
            new TaskItem { Id = 1, Title = "Design homepage", Status = "InProgress", Priority = "High", ProjectId = 1, DueDate = DateTime.Now.AddDays(7) },
            new TaskItem { Id = 2, Title = "Setup API", Status = "Done", Priority = "High", ProjectId = 1, DueDate = DateTime.Now.AddDays(-2) },
            new TaskItem { Id = 3, Title = "Write tests", Status = "Pending", Priority = "Medium", ProjectId = 2, DueDate = DateTime.Now.AddDays(3) }
        );
    }
}
