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
        public AppDbContext(DbContextOptions opts) : base(opts){}

        public DbSet<AccountAttachment> AccountAttachments { get; set; } = null!;
        public DbSet<AttachmentType> AttachmentTypes { get; set; } = null!;
        public DbSet<Profile> Profiles { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Field> Fields { get; set; } = null!;
        public DbSet<Stage> Stages { get; set; } = null!;
        public DbSet<StageAttachment> StagesAttachments { get; set; } = null!;
    }
}
