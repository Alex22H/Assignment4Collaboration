using Microsoft.EntityFrameworkCore;
using MVC_EF_Start.Models;

namespace MVC_EF_Start.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Student> Students { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Download> Downloads { get; set; }

  }
}