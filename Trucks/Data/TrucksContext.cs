﻿using Trucks.Data.Models;

namespace Trucks.Data
{
    using Microsoft.EntityFrameworkCore;

    public class TrucksContext : DbContext
    {
        public TrucksContext()
        {
        }

        public TrucksContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientTruck> ClientsTrucks { get; set; } = null!;
        public virtual DbSet<Despatcher> Despatchers { get; set; } = null!;
        public virtual DbSet<Truck> Trucks { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientTruck>(e =>
                e.HasKey(k => new { k.ClientId, k.TruckId }));
        }
    }
}
