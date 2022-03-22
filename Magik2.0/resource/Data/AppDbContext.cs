using Microsoft.EntityFrameworkCore;
using Resource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions opts) : base(opts)
        {
            Database.EnsureCreated();
        }

        public DbSet<AccountFile> AccountFiles { get; set; } = null!;
        public DbSet<FileType> FileTypes { get; set; } = null!;
        public DbSet<Profile> Profiles { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<ProjectType> ProjectTypes { get; set; } = null!;
        public DbSet<Field> Fields { get; set; } = null!;
        public DbSet<Stage> Stages { get; set; } = null!;
        public DbSet<StageFile> StagesFiles { get; set; } = null!;
    }
}
