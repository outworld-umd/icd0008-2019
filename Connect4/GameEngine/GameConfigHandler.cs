using System.Linq;
using System.Text.Json;
using DAL;

namespace GameEngine {

    public static class GameConfigHandler {

        public static void SaveConfig(GameSettings settings) {
            using var db = new AppDbContext();
            if (db.Settings.Any())
                db.Settings.Update(new SettingsState
                    {BoardHeight = settings.BoardHeight, BoardWidth = settings.BoardWidth});
            else
                db.Settings.Add(
                    new SettingsState {BoardHeight = settings.BoardHeight, BoardWidth = settings.BoardWidth});
            db.SaveChanges();
        }

        public static GameSettings? LoadConfig() {
            GameSettings? settings = null;
            using (var db = new AppDbContext()) {
                var state = db.Settings.Find(1);
                if (state != null)
                    settings = new GameSettings {BoardHeight = state.BoardHeight, BoardWidth = state.BoardWidth};
            }
            return settings;
        }
    }

}