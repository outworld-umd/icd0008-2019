using Newtonsoft.Json;

namespace GameEngine {

    public class GameSettings {

        public static readonly GameSettings Settings = GameConfigHandler.LoadConfig() ?? new GameSettings();

        [JsonProperty] public int BoardHeight { get; set; } = 6;
        [JsonProperty] public int BoardWidth { get; set; } = 7;

    }

}