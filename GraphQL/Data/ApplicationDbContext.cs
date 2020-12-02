using Microsoft.EntityFrameworkCore;

namespace Server.GraphQL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();

            // Relations.

        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Activity> Activities { get; set; } = default!;
        public DbSet<Exercise> Exercises { get; set; } = default!;

    }
}