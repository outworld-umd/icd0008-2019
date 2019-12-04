
using Newtonsoft.Json;

namespace GameEngine {
    
    public class GameSettings {

        public static readonly GameSettings Settings = GameConfigHandler.LoadConfig() ?? new GameSettings();
        
        [JsonProperty]
        public int BoardHeight { get; set; } = 6;
        [JsonProperty]
        public int BoardWidth { get; set; } = 7;

        public static string ChangeWidth() {
            var (wMin, wMax) = (7, 14);
            Settings.BoardWidth = InputHandler.GetUserIntInput($"Current width is {Settings.BoardWidth}.\nEnter board width (Type X to set default):", wMin, wMax,
                                      $"Width must be between {wMin} and {wMax}", true) ?? 7;
            GameConfigHandler.SaveConfig(Settings);
            return "";
        }

        public static string ChangeHeight() {
            var (hMin, hMax) = (6, 12);
            Settings.BoardHeight = InputHandler.GetUserIntInput($"Current height is {Settings.BoardHeight}.\nEnter board height (Type X to set default):", hMin, hMax,
                                       $"Height must be between {hMin} and {hMax}", true) ?? 6;
            GameConfigHandler.SaveConfig(Settings);
            return "";
        }

        public static string SetDefaults() {
            Settings.BoardHeight = 6;
            Settings.BoardWidth = 7;
            GameConfigHandler.SaveConfig(Settings);
            return "";
        }
        
    }
}