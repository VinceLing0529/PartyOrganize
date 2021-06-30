using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    [NotMapped] // don't add table to db
    public class LoginUser
    {
        [Required(ErrorMessage = "is required.")]
        
        [Display(Name = "Phone")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "is required.")]
        // [MinLength(8, ErrorMessage = "must be at least 8 characters")]
        [DataType(DataType.Password)] 
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }
    }
}