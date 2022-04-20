using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TruckApplication.Models;

namespace TruckApplication.Data
{
    public class TruckApplicationContext : DbContext
    {
        public TruckApplicationContext (DbContextOptions<TruckApplicationContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Model>()
                .Property(p => p.Description)
                .HasMaxLength(2);

            modelBuilder.Entity<Trucks>()
                .HasOne<Model>(s => s.Model)
                .WithMany(g => g.Trucks)
                .HasForeignKey(s => s.ModelId);
        }
        public virtual DbSet<TruckApplication.Models.Model> Model { get; set; }
        public virtual DbSet<TruckApplication.Models.Trucks> Trucks { get; set; }

    }
}
