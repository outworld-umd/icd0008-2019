
namespace GameEngine {
    
    public class GameSettings {
        public int BoardHeight { get; set; } = 6;
        public int BoardWidth { get; set; } = 7;

        public string ChangeWidth() {
            var (wMin, wMax) = (7, 14);
            BoardWidth = InputHandler.GetUserIntInput($"Current width is {BoardWidth}.\nEnter board width (Type X to set default):", wMin, wMax,
                $"Width must be between {wMin} and {wMax}", true) ?? 7;
            GameConfigHandler.SaveConfig(this);
            return "";
        }
        
        public string ChangeHeight() {
            var (hMin, hMax) = (6, 12);
            BoardHeight = InputHandler.GetUserIntInput($"Current height is {BoardHeight}.\nEnter board height (Type X to set default):", hMin, hMax,
                $"Height must be between {hMin} and {hMax}", true) ?? 6;
            GameConfigHandler.SaveConfig(this);
            return "";
        }

        public string SetDefaults() {
            BoardHeight = 6;
            BoardWidth = 7;
            GameConfigHandler.SaveConfig(this);
            return "";
        }
    }
}