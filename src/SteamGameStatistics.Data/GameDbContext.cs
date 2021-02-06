﻿using Microsoft.EntityFrameworkCore;
using SteamGameStatistics.Data.DAOs;

namespace SteamGameStatistics.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        { }

        public DbSet<PlayerDao> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerDao>().ToTable("Player");
        }
    }
}

// Package manager console run the following command.
// Add-Migration SteamGameStatistics.Data.GameDbContext
// If successful we need to apply that migration so run the next command:
// update-database