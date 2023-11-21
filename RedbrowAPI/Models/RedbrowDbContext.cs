using Microsoft.EntityFrameworkCore;
using System;

namespace RedbrowAPI.Models
{
    public class RedbrowDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }

        public RedbrowDbContext(DbContextOptions<RedbrowDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
