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

        public DbSet<AccountFile> AccountFiles { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectArea> ProjectAreas { get; set; }
        public DbSet<ProjectData> ProjectsData { get; set; }
        public DbSet<ProjectPart> ProjectParts { get; set; }
        public DbSet<ProjectPartData> ProjectPartsData { get; set; }
    }
}
