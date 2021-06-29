using System.ComponentModel.DataAnnotations;

namespace ActivityCenter.Models
{
    public class Phone
    {
        [Key]
        public int PhoneId{get;set;}
        [Required(ErrorMessage = "You must provide a phone number")]
[Display(Name = "Home Phone")]
[DataType(DataType.PhoneNumber)]
[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
public string Number { get; set; }
    
    }
}