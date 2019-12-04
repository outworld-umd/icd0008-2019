using System.ComponentModel.DataAnnotations;

namespace DAL {

    public class GameState {
        [Key] public string Name { get; set; }
        [Required] public string Data { get; set; }
    }
}