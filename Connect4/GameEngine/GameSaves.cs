using System.Linq;
using DAL;
using Newtonsoft.Json;

namespace GameEngine {

    public static class GameSaves {

        public static string DefaultName { get; } = "save";

        public static GameState? LoadSave(string? name) {
            using (var db = new AppDbContext()) {
                var gameState = db.Games.Find(name ?? DefaultName);
                if (gameState != null) return gameState;
            }
            return null;
        }

        public static string[] GetPlayableSaves() {
            using var db = new AppDbContext();
            return db.Games.Where(x => x.Winner == 0).Select(x => x.Name).ToArray();
        }
        
        public static string[] GetFinishedSaves() {
            using var db = new AppDbContext();
            return db.Games.Where(x => x.Winner != 0).Select(x => x.Name).ToArray();
        }

        public static void Save(Game game, string? name, int mode) {
            using var db = new AppDbContext();
            if (db.Games.Any(g => g.Name == (name ?? DefaultName)))
                db.Games.Update(new GameState
                    {Name = name ?? DefaultName, Data = JsonConvert.SerializeObject(game), Opponent = mode});
            else
                db.Games.Add(new GameState
                    {Name = name ?? DefaultName, Data = JsonConvert.SerializeObject(game), Opponent = mode});
            db.SaveChanges();
        }
    }

}