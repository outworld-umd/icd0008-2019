using System.IO;
using Newtonsoft.Json;

namespace GameEngine {

    public class GameSaves {

        private const string DefaultFileName = "save";

        public static Game LoadSave(string fileName) {
            if (!File.Exists($"saves/{fileName ?? DefaultFileName}.json")) return null;
            var json = File.ReadAllText($"saves/{fileName ?? DefaultFileName}.json");
            return JsonConvert.DeserializeObject<Game>(json);
        }

        public static void Save(Game game, string filename) {
            using (var writer = File.CreateText($"saves/{filename ?? DefaultFileName}.json")) {
                writer.Write(JsonConvert.SerializeObject(game));
                writer.Dispose();
            }
        }
    }

}