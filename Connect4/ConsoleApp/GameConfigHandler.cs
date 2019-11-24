using System.Linq;
using System.Text.Json;
using DAL;
using GameEngine;

namespace ConsoleApp {

    public static class GameConfigHandler {

        public static void SaveConfig(GameSettings settings) {
            using (var db = new AppDbContext()) {
                var jsonString = JsonSerializer.Serialize(settings);
                if (db.Settings.Any()) 
                    db.Settings.Update(new AppDbContext.SettingsState {SettingsId = 1, Data = jsonString});
                else db.Settings.Add(new AppDbContext.SettingsState {SettingsId = 1, Data = jsonString});
                db.SaveChanges();
            }
        }

        public static GameSettings? LoadConfig() {
            GameSettings? settings = null;
            using (var db = new AppDbContext()) {
                var state = db.Settings.Find(1);
                if (state != null) settings = JsonSerializer.Deserialize<GameSettings>(state.Data);
            }
            return settings;
        }
    }

}