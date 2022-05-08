using Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions opts) : base(opts){}

        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<MessageType> MessageTypes { get; set; } = null!;
    }
}
