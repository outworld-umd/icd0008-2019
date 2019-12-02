using System.Linq;
using DAL;
using Newtonsoft.Json;

namespace GameEngine {

    public class GameSaves {

        private const string DefaultName = "save";

        public static Game? LoadSave(string? name) {
            Game? game = null;
            using (var db = new AppDbContext()) {
                var gameState = db.Games.Find(name ?? DefaultName);
                if (gameState != null) game = JsonConvert.DeserializeObject<Game>(gameState.Data);
            }
            return game;
        }

        public static string[] GetSaves() {
            using (var db = new AppDbContext()) {
                return db.Games.Select(x => x.Name).ToArray();
            }
        }

        public static void Save(Game game, string? name) {
            using (var db = new AppDbContext()) {
                if (GetSaves().Contains(name ?? DefaultName))
                    db.Games.Update(new AppDbContext.GameState
                        {Name = name ?? DefaultName, Data = JsonConvert.SerializeObject(game)});
                else db.Games.Add(new AppDbContext.GameState
                    {Name = name ?? DefaultName, Data = JsonConvert.SerializeObject(game)});
                db.SaveChanges();
            }
        }
    }

}