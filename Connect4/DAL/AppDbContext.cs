
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace DAL {

    public class AppDbContext : DbContext {
        
        public DbSet<GameState> Games { get; set; }
        public DbSet<SettingsState> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var source =
                $"Data Source={Directory.GetCurrentDirectory().Replace("ConsoleApp\\bin\\Debug\\netcoreapp3.1", "").Replace("WebApp", "")}DAL\\data.db";
            Console.WriteLine(source);
            optionsBuilder.UseSqlite(source);

        }
        
    }

}