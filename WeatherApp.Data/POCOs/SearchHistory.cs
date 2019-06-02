using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WeatherApp.Data
{ 
    [Table("SearchHistory", Schema = "dbo")]
    public class SearchHistory
    {
        [Required]
        [Key]
        [Display(Name = "Member Id")]
        public int MemberId { get; set; }

        [Required]
        [Key]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}
