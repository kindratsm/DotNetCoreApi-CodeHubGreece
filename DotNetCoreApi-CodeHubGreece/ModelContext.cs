using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DotNetCoreApi_CodeHubGreece
{
    public class ModelContext : DbContext
    {

        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Country> Countries { get; set; }
        public DbSet<Models.Customer> Customers { get; set; }
        public DbSet<Models.Version> Versions { get; set; }
        public DbSet<Models.VersionNoteType> VersionNoteTypes { get; set; }
        public DbSet<Models.VersionNote> VersionNotes { get; set; }
        public DbSet<Models.Installation> Installations { get; set; }
        public DbSet<Models.InstallationNote> InstallationNotes { get; set; }

        public ModelContext()
        {
            // Check DB migration
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=codehub.db");
        }

    }
}
