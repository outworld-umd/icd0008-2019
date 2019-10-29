using System.IO;
using Newtonsoft.Json;

namespace GameEngine {

    public class GameSaves {

        private const string DefaultFileName = "save";

        public static Game? LoadSave(string? filename) {
            if (!File.Exists($"saves/{filename ?? DefaultFileName}.json")) return null;
            var json = File.ReadAllText($"saves/{filename ?? DefaultFileName}.json");
            return JsonConvert.DeserializeObject<Game>(json);
        }

        public static void Save(Game game, string? filename) {
            using (var writer = File.CreateText($"saves/{filename ?? DefaultFileName}.json")) {
                writer.Write(JsonConvert.SerializeObject(game));
                writer.Dispose();
            }
        }
    }

}