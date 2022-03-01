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
        public DbSet<ProjectArea> ProjectAreas { get; set; } = null!;
        public DbSet<ProjectData> ProjectsData { get; set; } = null!;
        public DbSet<ProjectPart> ProjectParts { get; set; } = null!;
        public DbSet<ProjectPartData> ProjectPartsData { get; set; } = null!;
    }
}
