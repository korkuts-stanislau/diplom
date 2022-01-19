using MagikAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagikAPI.Data
{
    public class MagikContext : DbContext
    {
        public MagikContext(DbContextOptions<MagikContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProjectArea> ProjectAreas { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectPart> ProjectParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Profile>()
                .HasOne(a => a.Account)
                .WithOne(p => p.Profile)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<ProjectArea>()
                .HasOne(p => p.Account)
                .WithMany(a => a.ProjectAreas)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Project>()
                .HasOne(p => p.ProjectArea)
                .WithMany(p => p.Projects)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<ProjectPart>()
                .HasOne(p => p.Project)
                .WithMany(p => p.ProjectParts)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
