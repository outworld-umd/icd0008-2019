using System.ComponentModel.DataAnnotations;

namespace DAL {

    public class SettingsState {
        [Key] public int SettingsId { get; set; } = 1;
        
        [Required]
        [Range(6, 12)]
        public int BoardHeight { get; set; }
        
        [Required]
        [Range(7, 14)]
        public int BoardWidth { get; set; }
    }

}