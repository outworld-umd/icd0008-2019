using System.ComponentModel.DataAnnotations;

namespace DAL {

    public class SettingsState {
        [Key] public int SettingsId { get; set; } = 1;
        [Required] public int BoardHeight { get; set; }
        [Required] public int BoardWidth { get; set; }
    }

}