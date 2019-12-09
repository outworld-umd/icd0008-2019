using System.ComponentModel.DataAnnotations;

namespace DAL {

    public class GameState {
        [Key] [Required] [MaxLength(18)] public string Name { get; set; }
        [Required] public string Data { get; set; }

        public int Winner { get; set; } = 0;

        [Required(ErrorMessage = "You must choose your opponent!")]
        public int Opponent { get; set; }
    }

}