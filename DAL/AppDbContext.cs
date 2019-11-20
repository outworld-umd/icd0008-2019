
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace DAL {

    public class AppDbContext : DbContext {
        
        public DbSet<GameState> Games { get; set; }
        public DbSet<SettingsState> Settings { get; set; }

        public class GameState {
            [Key] public string Name { get; set; }
            [Required] public string Data { get; set; }
        }

        public class SettingsState {
            [Key] public int SettingsId { get; set; }
            [Required] public string Data { get; set; }
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var source =
                $"Data Source={Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\netcoreapp3.0", "")}\\database.db";
            optionsBuilder.UseSqlite(source);

        }
        
    }

}