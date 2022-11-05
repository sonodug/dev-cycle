using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wpf_game_dev_cycle.Model
{
    [Table("admin_account", Schema = "company")]
    public class admin_account
    {
        [Key]
        public int Account_id { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; set; }
        // public string? Name { get; set; }
        // public string? LastName { get; set; }
        // public string? Email { get; set; }
        // public string? Phone { get; set; } 
        // public string? Verification_Code { get; set; }
    }
}