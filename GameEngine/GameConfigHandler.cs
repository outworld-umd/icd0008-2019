using System;
using System.IO;
using System.Text.Json;

namespace GameEngine {

    public static class GameConfigHandler {

        private const string FileName = "gamesettings.json";

        public static void SaveConfig(GameSettings settings, string fileName = FileName) {
            using (var writer = File.CreateText(fileName)) {
                var jsonString = JsonSerializer.Serialize(settings);
                writer.Write(jsonString);
            }
        }

        public static GameSettings? LoadConfig(string fileName = FileName) {
            try {
                var jsonString = File.ReadAllText(fileName);
                var res = JsonSerializer.Deserialize<GameSettings>(jsonString);
                return res;
            } catch (Exception) {
                return null;
            }
        }
    }

}