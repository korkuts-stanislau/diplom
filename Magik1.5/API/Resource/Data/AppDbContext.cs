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

        public Profile Profile { get; set; }
        public ProjectArea ProjectArea { get; set; }
        public Project Project { get; set; }
        public ProjectPart ProjectPart { get; set; }
    }
}
