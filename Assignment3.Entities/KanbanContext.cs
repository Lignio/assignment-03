using Microsoft.EntityFrameworkCore;

namespace Assignment3.Entities;

public class KanbanContext : DbContext
{
    public DbSet<Task> Tasks => Set<Task>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Tag> Tags => Set<Tag>();


    public KanbanContext(DbContextOptions<KanbanContext> options) : base(options) {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Task>().HasIndex(c=>c.Id).IsUnique();
        modelBuilder.Entity<Task>().Property(e => e.state).HasConversion(
            v => v.ToString(),
            v => (State)Enum.Parse(typeof(State), v));
        modelBuilder.Entity<User>().HasIndex(c => c.Email).IsUnique();  
        modelBuilder.Entity<Tag>().HasIndex(c=>c.Name).IsUnique();  


    }

}
