using HangfireEmailSchedule.Models;
using Microsoft.EntityFrameworkCore;

namespace HangfireEmailSchedule.Repository
{
    public class DBContext : DbContext{
        public DBContext(DbContextOptions<DBContext> options) : base(options) {}

        public DbSet<Post> Posts { get; set; }
    }
}
