using AlignBeckendApi.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlignBeckendApi.Dal
{
    public class MI6DbContext : DbContext
    {
        public MI6DbContext(DbContextOptions<MI6DbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MissionEntity>();
        }

        public DbSet<MissionEntity> Missions { get; set; }
    }
}
