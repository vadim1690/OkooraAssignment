using Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data
{
    public class RateDbContext : DbContext
    {
        public RateDbContext(DbContextOptions<RateDbContext> options) : base(options)
        {

        }

        public DbSet<RatePair> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RatePair>(entity =>
            {
                entity.Property(e => e.PairName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Rate).HasPrecision(18, 4);
                entity.Property(e => e.LastUpdate).IsRequired();
            });
        }
    }
}
