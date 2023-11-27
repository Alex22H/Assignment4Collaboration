using Microsoft.EntityFrameworkCore;
using MVC_EF_Start.Models;

namespace MVC_EF_Start.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Graduate> Graduates { get; set; }
        public DbSet<ProjectDocument> ProjectDocuments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Download> Downloads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Graduate>()
                .ToTable("Graduates"); // Specify the actual table name

            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }

    }

  
}
