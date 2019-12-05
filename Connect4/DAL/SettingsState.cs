using System.ComponentModel.DataAnnotations;

namespace DAL {

    public class SettingsState {
        [Key] public int SettingsId { get; set; } = 1;
        
        [Required (ErrorMessage = "Field is mandatory!")]
        [Range(6, 12, ErrorMessage = "Board Height can only be in range 6 to 12!")]
        public int BoardHeight { get; set; }
        
        [Required (ErrorMessage = "Field is mandatory!")]
        [Range(7, 14, ErrorMessage = "Board Width can only be in range 7 to 14!")]
        public int BoardWidth { get; set; }
    }

}