using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp.Data
{
    [Table("Users", Schema = "dbo")]
    public class User
    {
        [Required]
        [Key]
        [Display(Name = "Member Id")]
        public int MemberId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
