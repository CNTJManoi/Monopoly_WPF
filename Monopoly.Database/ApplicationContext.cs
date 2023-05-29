﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Monopoly.Database.Models;
using Monopoly.Logic;

namespace Monopoly.Database
{
    internal class ApplicationContext : DbContext
    {
        private readonly StreamWriter _logStream = new("database.log", true);
        public DbSet<Player> Players => Set<Player>();
        public DbSet<SaveGameModel> SavedGames => Set<SaveGameModel>();
        public DbSet<Statictics> Statictics => Set<Statictics>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Пересмотреть
            modelBuilder.Entity<Player>(
                b =>
                {
                    b.HasKey("Id");
                    b.Property(e => e.Name);
                });
            modelBuilder.Entity<SaveGameModel>(
                b =>
                {
                    b.HasKey("Id");
                    b.Property(e => e.JsonSaveGame);
                });
            modelBuilder.Entity<Statictics>(
                b =>
                {
                    b.HasKey("Id");
                    b.Property(e => e.CountMoves);
                    b.Property(e => e.TimeOfGame);
                    b.Property(e => e.SumPlayersScore);
                });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer("server=localhost;database=Hospital;user=root;password=");
                optionsBuilder.LogTo(_logStream.WriteLine);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка подключения к базе данных " + e.Data);
                Environment.Exit(1);
            }
        }
        public override void Dispose()
        {
            base.Dispose();
            _logStream.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await _logStream.DisposeAsync();
        }
    }
}