using System;
using Newtonsoft.Json;

namespace GameEngine {
    public class GameSaves {

        private const string DefaultFileName = "save";
        
        public static Game LoadSave(string fileName) {
            if (!System.IO.File.Exists($"saves/{fileName ?? DefaultFileName}.json")) return null;
            var json = System.IO.File.ReadAllText($"saves/{fileName ?? DefaultFileName}.json");
            return JsonConvert.DeserializeObject<Game>(json);
        }
        
        public static void Save(Game game, string filename) {
            using (var writer = System.IO.File.CreateText($"saves/{filename ?? DefaultFileName}.json")) {
                writer.Write(JsonConvert.SerializeObject(game));
                writer.Dispose();
            }

        }
        
    }

}