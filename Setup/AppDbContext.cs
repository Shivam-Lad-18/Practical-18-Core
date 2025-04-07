using Microsoft.EntityFrameworkCore;
using Setup.Models;
using System.Collections.Generic;

namespace Setup
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
